using Application.DTOs.User;
using Application.UseCases.USER.Commands;

namespace Application.Interfaces.Handlers.User
{
    public interface ICreateUserHandler
    {
        Task<string> Handle(CreateUserCommand command);
    }
}
