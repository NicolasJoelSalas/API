using Application.Interfaces.Handlers.Reservation;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases
{
    public class ConfirmPaymentHandler : IConfirmPaymentHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IAudit_LogRepository _auditLogRepository;

        public ConfirmPaymentHandler(
            IReservationRepository reservationRepository,
            ISeatRepository seatRepository,
            IAudit_LogRepository auditLogRepository)
        {
            _reservationRepository = reservationRepository;
            _seatRepository = seatRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<string> Handle(List<Guid> reservationIds)
        {
            if (reservationIds == null || !reservationIds.Any())
                return "No hay reservas para procesar";

            // 1. Obtener reservas
            var reservations = await _reservationRepository.GetAllByIdAsync(reservationIds);

            if (reservations == null || !reservations.Any())
                return "Reservas no encontradas";

            var userId = reservations.First().UserId;

            // 2. Actualizar reservas a Paid
            foreach (var reservation in reservations)
            {
                reservation.Status = ReservationStatus.Paid.ToString();
                await _reservationRepository.UpdateAsync(reservation);
            }

            // 3. Marcar seats como Sold
            foreach (var reservation in reservations)
            {
                var seat = await _seatRepository.GetByIdAsync(reservation.SeatId);

                if (seat != null)
                {
                    seat.Status = SeatStatus.Sold.ToString();
                    await _seatRepository.UpdateAsync(seat);
                }
            }

            // 4. Crear Audit Log
            var auditLog = new Domain.Entities.AUDIT_LOG
            {
                Action = "Pago Confirmado",
                UserId = userId,
                EntityType = "Reservation",
                EntityId = string.Join(",", reservationIds),
                Details = $"Pago confirmado para {reservationIds.Count} reservas",
                CreatedAt = DateTime.UtcNow
            };

            await _auditLogRepository.AddAsync(auditLog);

            // 5. Guardar cambios
            await _reservationRepository.SaveChangesAsync();

            return "Pago confirmado y reservas actualizadas";
        }
    }
}