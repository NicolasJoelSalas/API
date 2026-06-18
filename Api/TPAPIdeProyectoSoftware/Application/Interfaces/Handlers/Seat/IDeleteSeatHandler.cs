using Application.DTOs.User;
using Application.UseCases.USER.Commands;

namespace Application.Interfaces.Handlers.User
{
    public interface IDeleteSeatHandler
    {
        Task<string> Handle(DeleteSeatCommand command);
    }
}
