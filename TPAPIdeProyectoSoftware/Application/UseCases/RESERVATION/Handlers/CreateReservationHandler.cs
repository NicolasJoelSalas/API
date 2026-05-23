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

            if (command.SeatIds == null || !command.SeatIds.Any())
                throw new Exception("No se enviaron butacas");

            // Validar usuario
            var user = await _userRepository.GetByIdAsync(command.UserId);

            if (user == null)
                throw new Exception("El usuario no existe");

            // Audit inicial
            await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
            {
                UserId = command.UserId,
                Action = "Intento de reserva",
                EntityType = "Reservation",
                EntityId = string.Join(",", command.SeatIds),
                Details = $"UsuarioId: {command.UserId}, Seats: {string.Join(",", command.SeatIds)}",
                CreatedAt = DateTime.UtcNow
            });

            // Traer todas las butacas
            var seats = new List<Domain.Entities.SEAT>();

            foreach (var seatId in command.SeatIds)
            {
                var seat = await _seatRepository.GetByIdAsync(seatId);

                if (seat == null)
                    throw new Exception($"La butaca {seatId} no existe");

                seats.Add(seat);
            }

            // Validar disponibilidad
            var occupiedSeats = seats
                .Where(s => s.Status == SeatStatus.Reserved.ToString() ||
                            s.Status == SeatStatus.Sold.ToString())
                .ToList();

            if (occupiedSeats.Any())
            {
                var occupiedIds = string.Join(",", occupiedSeats.Select(s => s.Id));

                await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
                {
                    UserId = command.UserId,
                    Action = "Reserva rechazada",
                    EntityType = "Reservation",
                    EntityId = occupiedIds,
                    Details = "Hay butacas ocupadas",
                    CreatedAt = DateTime.UtcNow
                });

                throw new Exception($"Las siguientes butacas ya están ocupadas: {occupiedIds}");
            }

            try
            {
                foreach (var seat in seats)
                {
                    seat.Status = SeatStatus.Reserved.ToString();
                    await _seatRepository.UpdateAsync(seat);


                    var reservation = new Domain.Entities.RESERVATION
                    {
                        UserId = command.UserId,
                        Status = ReservationStatus.Pending.ToString(),
                        ReservedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                        SEAT = seat
                    };


                    await _reservationRepository.AddAsync(reservation);

                    await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
                    {
                        UserId = command.UserId,
                        Action = "Reserva exitosa",
                        EntityType = "Reservation",
                        EntityId = reservation.Id.ToString(),
                        Details = $"UsuarioId: {command.UserId}, Seats: {string.Join(",", command.SeatIds)}",
                        CreatedAt = DateTime.UtcNow
                    });


                    await _reservationRepository.SaveChangesAsync();

                    return reservation.Id;
                }
            }

            catch (Exception ex)
            {

                await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
                {
                    UserId = command.UserId,
                    Action = "Error en reserva",
                    EntityType = "Reservation",
                    EntityId = string.Join(",", command.SeatIds),
                    Details = ex.Message,
                    CreatedAt = DateTime.UtcNow
                });

                throw;
            }
            return Guid.Empty;
        }
    }
}