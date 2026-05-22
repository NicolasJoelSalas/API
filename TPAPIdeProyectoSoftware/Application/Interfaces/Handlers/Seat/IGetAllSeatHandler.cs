using Application.DTOs.User;
using Application.UseCases.USER.Queries;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetAllSeatHandler
    {
        Task<(List<SeatResponseDto> seats, string message)> Handle(GetAllSeatQuery query);
    }
}
