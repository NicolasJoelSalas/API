using Application.Interfaces.Handlers.Reservation;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.RESERVATION.Commands;
using Domain.Entities;
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

            if (command.SeatIds == null || !command.SeatIds.Any())
                throw new Exception("No se enviaron butacas");

            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null)
                throw new Exception("Usuario no existe");

            await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
            {
                UserId = command.UserId,
                Action = "Intento de reserva",
                EntityType = "Reservation",
                EntityId = string.Join(",", command.SeatIds),
                Details = "Inicio de proceso",
                CreatedAt = DateTime.UtcNow
            });

            var reservations = new List<Domain.Entities.RESERVATION>();

            foreach (var seatId in command.SeatIds)
            {
                var seat = await _seatRepository.GetByIdAsync(seatId);

                if (seat == null)
                    continue;

                if (seat.Status == SeatStatus.Reserved.ToString() ||
                    seat.Status == SeatStatus.Sold.ToString())
                {
                    await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
                    {
                        UserId = command.UserId,
                        Action = "Rechazada",
                        EntityType = "Reservation",
                        EntityId = seatId.ToString(),
                        Details = "Ya ocupada",
                        CreatedAt = DateTime.UtcNow
                    });

                    continue;
                }

                try
                {
                    seat.Status = SeatStatus.Reserved.ToString();
                    seat.Version++; // importante

                    await _seatRepository.UpdateAsync(seat);

                    var reservation = new Domain.Entities.RESERVATION
                    {
                        Id = Guid.NewGuid(),
                        UserId = command.UserId,
                        SeatId = seatId,
                        Status = ReservationStatus.Pending.ToString(),
                        ReservedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                    };

                    await _reservationRepository.AddAsync(reservation);

                    reservations.Add(reservation);

                    await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
                    {
                        UserId = command.UserId,
                        Action = "OK",
                        EntityType = "Reservation",
                        EntityId = reservation.Id.ToString(),
                        Details = "Reserva creada",
                        CreatedAt = DateTime.UtcNow
                    });
                }
                catch
                {
                    await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
                    {
                        UserId = command.UserId,
                        Action = "Concurrency",
                        EntityType = "Reservation",
                        EntityId = seatId.ToString(),
                        Details = "Otro usuario lo tomó primero",
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            if (!reservations.Any())
                throw new Exception("No se pudo reservar ninguna butaca");

            return reservations.Select(x => x.Id).ToList();
        }


    }
}