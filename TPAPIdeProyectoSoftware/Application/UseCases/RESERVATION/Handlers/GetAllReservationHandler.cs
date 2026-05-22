using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Queries;
using Application.UseCases.USER.Queries;

namespace Application.UseCases
{
    public class GetAllReservationHandler : IGetAllReservationHandler
    {
        private readonly IReservationRepository _reservationRepository;

        public GetAllReservationHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<(List<ReservationResponseDto> Reservations, string message)> Handle(GetAllReservationQuery query)
        {
            var reservations = await _reservationRepository.GetAllAsync();

            if (reservations == null || !reservations.Any())
                return (new List<ReservationResponseDto>(), "No hay reservas");

            var reservationDtos = reservations.Select(reservationEntity => new ReservationResponseDto
            {
                Id = reservationEntity.Id,
                UserId = reservationEntity.UserId,
                SeatId = reservationEntity.SeatId,
                Status = reservationEntity.Status,
                ReservedAt = reservationEntity.ReservedAt,
                ExpiresAt = reservationEntity.ExpiresAt
            }).ToList();

            return (reservationDtos, "OK");
        }
    }
}
