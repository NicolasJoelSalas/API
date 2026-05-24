using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.RESERVATION.Commands;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.RESERVATION.Handlers
{
    public class CreateReservationHandler : ICreateReservationHandler
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IAudit_LogRepository _auditLogRepository;

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

            var reservations = new List<Domain.Entities.RESERVATION>();

            foreach (var seatId in availableSeatIds)
            {
                try
                {
                    var seat =
                        await _seatRepository.GetByIdAsync(seatId);

                    if (seat == null)
                        continue;

                    if (seat.Status is "Reserved"
                        or "Sold")
                        continue;


                    await _seatRepository.UpdateStatusAsync(seatId, "Reserved");


                    reservations.Add(new Domain.Entities.RESERVATION
                    {
                        UserId = request.UserId,
                        SeatId = seatId,
                        Status = "Pending",
                        ReservedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(5)
                    });
                }
                finally
                {
                   Console.WriteLine("Reservation processed.");
                }
                //catch (DbUpdateConcurrencyException)
                //{
                //    continue;
                //}
            }

            if (!reservations.Any())
                throw new Exception(
                    "No se pudo reservar ninguna butaca");

            return await _reservationRepository.AddAllAsync(reservations);

            
        }
    }
}