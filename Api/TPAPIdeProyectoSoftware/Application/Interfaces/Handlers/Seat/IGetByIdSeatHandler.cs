using Application.DTOs.User;
using Application.UseCases.USER.Queries;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetByIdSeatHandler
    {
        Task<(SeatResponseDto? seat, string message)> Handle(GetByIdSeatQuery query);

    }
}
