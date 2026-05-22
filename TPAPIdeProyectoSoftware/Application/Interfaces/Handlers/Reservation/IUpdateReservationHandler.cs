using Application.DTOs.User;

namespace Application.Interfaces.Handlers
{
    public interface IUpdateReservationHandler
    {
        Task<string> Handle(UpdateReservationCommand command);
    }
}
