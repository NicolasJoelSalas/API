using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.RESERVATION.Commands;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;


namespace Application.UseCases.RESERVATION.Handlers
{
    public class CreateReservationHandler : ICreateReservationHandler
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IAudit_LogRepository _auditLogRepository;
        private const int ReservationTtlMinutes = 5;
        public CreateReservationHandler(
            ISeatRepository seatRepository,
            IUserRepository userRepository,
            IReservationRepository reservationRepository,
            IAudit_LogRepository auditLogRepository)
        {
            _seatRepository = seatRepository;
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<List<Guid>> Handle(
    CreateReservationCommand request)
        {

            if (request.UserId <= 0)
                throw new Exception("El Id del usuario es obligatorio");

            if (request.SeatIds == null || !request.SeatIds.Any())
                throw new Exception("No se enviaron butacas");

            var user = await _userRepository
                .GetByIdAsync(request.UserId);

            if (user == null)
                throw new Exception("Usuario inexistente");


            var reservedSeatIds =
                await _seatRepository
                    .GetReservedSeatIdsAsync(request.SeatIds);

            var availableSeatIds = request.SeatIds
                .Except(reservedSeatIds)
                .ToList();

            await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
            {
                UserId = request.UserId,
                Action = "Intento de reserva",
                EntityType = "Reservation",
                EntityId = string.Join(",", request.SeatIds),
                Details = "Inicio de proceso",
                CreatedAt = DateTime.UtcNow
            });

            var reservations = new List<Domain.Entities.RESERVATION>();

            foreach (var seatId in availableSeatIds)
            {
                var seat = await _seatRepository.GetByIdAsync(seatId);

                if (seat == null)
                    continue;

                var success = await _seatRepository.ReserveSeatAsync(seatId, seat.Version);

                if (!success)
                    continue;

                var reservation = new Domain.Entities.RESERVATION
                {
                    UserId = request.UserId,
                    SeatId = seatId,
                    Status = "Pending",
                    ReservedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(ReservationTtlMinutes)
                };

                reservations.Add(reservation);

                await _auditLogRepository.AddAsync(new Domain.Entities.AUDIT_LOG
                {
                    UserId = request.UserId,
                    Action = "Reserva exitosa",
                    EntityType = "Reservation",
                    EntityId = seatId.ToString(),
                    Details = "Reserva creada",
                    CreatedAt = DateTime.UtcNow
                });
            }

            if (!reservations.Any())
                throw new ReservationConflictException("No se pudo reservar ninguna butaca");

            return await _reservationRepository.AddAllAsync(reservations);


        }
    }
}