using Application.DTOs.User;
using Application.UseCases.USER.Queries;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetAllUserHandler
    {
        Task<(List<UserResponseDto> users, string message)> Handle(GetAllUserQuery query);
    }
}
