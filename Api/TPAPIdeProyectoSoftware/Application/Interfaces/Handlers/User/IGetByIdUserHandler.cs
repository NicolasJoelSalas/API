using Application.DTOs.User;
using Application.UseCases.USER.Queries;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetByIdUserHandler
    {
        Task<(UserResponseDto? user, string message)> Handle(GetByIdUserQuery query);

    }
}
