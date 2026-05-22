using Application.DTOs.Event;
using Application.DTOs.User;
using Application.UseCases.EVENT.Commands;

namespace Application.Interfaces.Handlers.Event
{
    public interface IUpdateEventHandler
    {
        Task<string> Handle(UpdateEventCommand command);
    }
}