using Application.DTOs.User;
using Application.UseCases.RESERVATION.Commands;
using Application.UseCases.USER.Commands;

namespace Application.Interfaces.Handlers.User
{
    public interface ICreateReservationHandler
    {
        Task<Guid> Handle(CreateReservationCommand command);
    }
}
