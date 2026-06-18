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
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmPaymentHandler(
            IReservationRepository reservationRepository,
            ISeatRepository seatRepository,
            IAudit_LogRepository auditLogRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _seatRepository = seatRepository;
            _auditLogRepository = auditLogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(List<Guid> reservationIds)
        {
            if (reservationIds == null || !reservationIds.Any())
                return "No hay reservas para procesar";
            var reservations = await _reservationRepository.GetAllByIdAsync(reservationIds);

            if (reservations == null || !reservations.Any())
                return "Reservas no encontradas";

            var userId = reservations.First().UserId;
            
            var horaActual= DateTime.UtcNow;

            foreach (var reservation in reservations)
            {
                if (reservation.Status != ReservationStatus.Pending.ToString())
                    return $"La reserva {reservation.Id} no está pendiente";

                if (reservation.ExpiresAt <= horaActual)
                    return $"La reserva {reservation.Id} ya expiró";
            }

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                

                foreach (var reservation in reservations)
                {
                    reservation.Status = ReservationStatus.Paid.ToString();

                    var seat = await _seatRepository.GetByIdAsync(reservation.SeatId);

                    if (seat == null)
                        throw new Exception("Asiento no encontrado");

                    seat.Status = SeatStatus.Sold.ToString();

                    await _reservationRepository.UpdateAsync(reservation);
                    await _seatRepository.UpdateAsync(seat);
                }

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

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                return "Pago confirmado y reservas actualizadas";
            }
            catch
            {

                var auditLog = new Domain.Entities.AUDIT_LOG
                {
                    Action = "Error al confirmar pago",
                    UserId = userId,
                    EntityType = "Reservation",
                    EntityId = string.Join(",", reservationIds),
                    Details = $"Error al confirmar pago para {reservationIds.Count} reservas",
                    CreatedAt = DateTime.UtcNow
                };

                await _auditLogRepository.AddAsync(auditLog);
                await _unitOfWork.RollbackTransactionAsync();
                return "Error al confirmar el pago";
            }
        }
    }
}