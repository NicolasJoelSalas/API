using Application.DTOs.User;
using Application.UseCases.USER.Commands;

namespace Application.Interfaces.Handlers.User
{
    public interface ICreateSeatHandler
    {
        Task<string> Handle(CreateSeatCommand command);
    }
}
