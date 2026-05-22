using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases.Reservation.Handlers
{
    public class CreateReservationHandler : ICreateReservationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IAudit_LogRepository _auditLogRepository;

        public CreateReservationHandler(
            IUserRepository userRepository,
            ISeatRepository seatRepository,
            IReservationRepository reservationRepository,
            IAudit_LogRepository auditLogRepository)
        {
            _userRepository = userRepository;
            _seatRepository = seatRepository;
            _reservationRepository = reservationRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<string> Handle(CreateReservationCommand commmand)
        {
            if (commmand == null)
                return "Datos inválidos";

            if (commmand.UserId <= 0)
                return "El Id del usuario es obligatorio";

            if (commmand.SeatId == Guid.Empty)
                return "El Id del asiento es obligatorio";

            var user = await _userRepository.GetByIdAsync(commmand.UserId);

            if (user == null)
                return "El Usuario no existe";

            var seat = await _seatRepository.GetByIdAsync(commmand.SeatId);

            if (seat == null)
                return "El asiento no existe";

            if (seat.Status == SeatStatus.Reserved.ToString())
                return "El asiento ya está reservado";

            if (seat.Status == SeatStatus.Sold.ToString())
                return "El asiento ya está vendido";

            var auditIntent = new Domain.Entities.AUDIT_LOG
            {
                UserId = commmand.UserId,
                Action = "Intento de reserva",
                EntityType = "Seat",
                EntityId = commmand.SeatId.ToString(),
                Details = $"UsuarioId: {commmand.UserId}, AsientoId: {commmand.SeatId}",
                CreatedAt = DateTime.UtcNow
            };

            await _auditLogRepository.AddAsync(auditIntent);

            seat.Status = SeatStatus.Reserved.ToString();
            await _seatRepository.UpdateAsync(seat);

            var reservation = new Domain.Entities.RESERVATION
            {
                UserId = commmand.UserId,
                SeatId = commmand.SeatId,
                Status = ReservationStatus.Pending.ToString(),
                ReservedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            await _reservationRepository.AddAsync(reservation);

            var auditSuccess = new Domain.Entities.AUDIT_LOG
            {
                UserId = commmand.UserId,
                Action = "Reserva exitosa",
                EntityType = "Reservation",
                EntityId = reservation.Id.ToString(),
                Details = $"UsuarioId: {commmand.UserId}, AsientoId: {commmand.SeatId}, ReservaId: {reservation.Id}",
                CreatedAt = DateTime.UtcNow
            };

            await _auditLogRepository.AddAsync(auditSuccess);

            return "OK";
        }
    }
}