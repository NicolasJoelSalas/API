using Application.Interfaces.Handlers.Reservation;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.RESERVATION.Commands;
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

        public async Task<Guid> Handle(CreateReservationCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.UserId <= 0)
                throw new Exception("El Id del usuario es obligatorio");

            if (command.SeatId == Guid.Empty)
                throw new Exception("El Id de la butaca es obligatorio");

            // Validar usuario
            var user = await _userRepository.GetByIdAsync(command.UserId);

            if (user == null)
                throw new Exception("El usuario no existe");

            // Validar asiento
            var seat = await _seatRepository.GetByIdAsync(command.SeatId);

            if (seat == null)
                throw new Exception("La butaca no existe");

            // Audit inicial
            await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
            {
                UserId = command.UserId,
                Action = "Intento de reserva",
                EntityType = "Reservation",
                EntityId = command.SeatId.ToString(),
                Details = $"UsuarioId: {command.UserId}, Seat: {command.SeatId}",
                CreatedAt = DateTime.UtcNow
            });

            try
            {
                var reservation = new Domain.Entities.RESERVATION
                {
                    UserId = command.UserId,
                    SeatId = command.SeatId,
                    Status = ReservationStatus.Pending.ToString(),
                    ReservedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                };

                await _reservationRepository.AddAsync(reservation);
                await _reservationRepository.SaveChangesAsync();

                await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
                {
                    UserId = command.UserId,
                    Action = "Reserva exitosa",
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"UsuarioId: {command.UserId}, Seat: {command.SeatId}",
                    CreatedAt = DateTime.UtcNow
                });

                return reservation.Id;
            }
            catch (Exception ex)
            {
                await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
                {
                    UserId = command.UserId,
                    Action = "Error en reserva",
                    EntityType = "Reservation",
                    EntityId = command.SeatId.ToString(),
                    Details = ex.Message,
                    CreatedAt = DateTime.UtcNow
                });

                throw;
            }
        }
    }
}