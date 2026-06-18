using Application.DTOs.Event;
using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Queries;
using Application.UseCases.USER.Queries;

namespace Application.UseCases
{
    public class GetByIdReservationHandler : IGetByIdReservationHandler
    {
        private readonly IReservationRepository _reservationRepository;

        public GetByIdReservationHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<(ReservationResponseDto Reservation, string message)> Handle(GetByIdReservationQuery query)
        {
            if (query == null)
                return (new ReservationResponseDto(), "Query inválida");

            if (query.Id == null)
                return (new ReservationResponseDto(), "Id inválido");

            var reservationEntity = await _reservationRepository.GetByIdAsync(query.Id);

            if (reservationEntity == null)
                return (new ReservationResponseDto(), "Reserva no encontrada");

            return (new ReservationResponseDto
            {
                Id = reservationEntity.Id,
                UserId = reservationEntity.UserId,
                SeatId = reservationEntity.SeatId,
                Status = reservationEntity.Status,
                ReservedAt = reservationEntity.ReservedAt,
                ExpiresAt = reservationEntity.ExpiresAt
            }, "OK");
        }

    }
}
