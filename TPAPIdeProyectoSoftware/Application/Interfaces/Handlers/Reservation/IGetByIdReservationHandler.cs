using Application.DTOs.User;
using Application.UseCases.USER.Queries;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetByIdReservationHandler
    {
        Task<(ReservationResponseDto Reservation, string message)> Handle(GetByIdReservationQuery query);

    }
}
