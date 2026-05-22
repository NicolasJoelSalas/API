using Application.DTOs.User;
using Application.UseCases.USER.Commands;

namespace Application.Interfaces.Handlers.User
{
    public interface IDeleteReservationHandler
    {
        Task<string> Handle(DeleteReservationCommand command);
    }
}
