using Application.DTOs.User;
using Application.UseCases.USER.Queries;

namespace Application.Interfaces.Handlers.User
{
    public interface ILoginUserHandler
    {
        Task<(bool Success, string Message, int? UserId, string? Username)> Handle(GetAllUserLoginQuery query);
    }
}
