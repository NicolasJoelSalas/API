using Application.DTOs.User;
using Application.UseCases.USER.Queries;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetAllReservationHandler
    {
        Task<(List<ReservationResponseDto> Reservations, string message)> Handle(GetAllReservationQuery query);
    }
}
