using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.RESERVATION.Commands;
using Domain.Enums;

namespace Application.UseCases.RESERVATION.Handlers
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

        public async Task<List<Guid>> Handle(CreateReservationCommand command)
        {
            if (command == null)
                throw new Exception("Datos inválidos");

            if (command.UserId <= 0)
                throw new Exception("El Id del usuario es obligatorio");

            if (command.SeatIds == null || !command.SeatIds.Any())
                throw new Exception("No se enviaron butacas");

            // 1. Validar usuario
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null)
                throw new Exception("El usuario no existe");

            // 2. Audit inicial
            var auditIntent = new Domain.Entities.AUDIT_LOG
            {
                UserId = command.UserId,
                Action = "Intento de reserva",
                EntityType = "Reservation",
                EntityId = string.Join(",", command.SeatIds),
                Details = $"UsuarioId: {command.UserId}, Seats: {string.Join(",", command.SeatIds)}",
                CreatedAt = DateTime.UtcNow
            };

            await _auditLogRepository.AddAsync(auditIntent);

            // 3. Obtener butacas ya reservadas
            var reservedSeatIds = await _reservationRepository.GetReservedSeatIdsAsync(command.SeatIds);

            var availableSeatIds = command.SeatIds
                .Except(reservedSeatIds)
                .ToList();

            if (!availableSeatIds.Any())
                throw new Exception("Todas las butacas ya están reservadas");

            var reservations = new List<Domain.Entities.RESERVATION>();

            foreach (var seatId in availableSeatIds)
            {
                try
                {
                    var seat = await _seatRepository.GetByIdAsync(seatId);

                    if (seat == null)
                        continue;

                    if (seat.Status == SeatStatus.Reserved.ToString() ||
                        seat.Status == SeatStatus.Sold.ToString())
                    {   
                        var auditConflict = new Domain.Entities.AUDIT_LOG
                        {
                            UserId = command.UserId,
                            Action = "Reserva rechazada",
                            EntityType = "Reservation",
                            EntityId = seatId.ToString(),
                            Details = "La butaca ya está reservada",
                            CreatedAt = DateTime.UtcNow
                        };

                        await _auditLogRepository.AddAsync(auditConflict);
                        continue;
                    }

                    // 4. Reservar butaca
                    seat.Status = SeatStatus.Reserved.ToString();
                    seat.Version++;

                    await _seatRepository.UpdateAsync(seat);

                    // 5. Crear reserva
                    var reservation = new Domain.Entities.RESERVATION
                    {
                        UserId = command.UserId,
                        SeatId = seatId,
                        Status = ReservationStatus.Pending.ToString(),
                        ReservedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                    };

                    reservations.Add(reservation);

                    // 6. Audit éxito
                    var auditSuccess = new Domain.Entities.AUDIT_LOG
                    {
                        UserId = command.UserId,
                        Action = "Reserva exitosa",
                        EntityType = "Reservation",
                        EntityId = seatId.ToString(),
                        Details = $"UsuarioId: {command.UserId}, AsientoId: {seatId}",
                        CreatedAt = DateTime.UtcNow
                    };

                    await _auditLogRepository.AddAsync(auditSuccess);
                }
                catch (Exception)
                {
                    var auditConflict = new Domain.Entities.AUDIT_LOG
                    {
                        UserId = command.UserId,
                        Action = "Conflicto de concurrencia",
                        EntityType = "Reservation",
                        EntityId = seatId.ToString(),
                        Details = "Otro usuario reservó la butaca primero",
                        CreatedAt = DateTime.UtcNow
                    };

                    await _auditLogRepository.AddAsync(auditConflict);
                }
            }

            if (!reservations.Any())
                throw new Exception("No se pudo reservar ninguna butaca");

            // 7. Guardar reservas
            await _reservationRepository.AddRangeAsync(reservations);

            // 8. Guardar cambios
            await _reservationRepository.SaveChangesAsync();

            return reservations.Select(r => r.Id).ToList();
        }
    }
}