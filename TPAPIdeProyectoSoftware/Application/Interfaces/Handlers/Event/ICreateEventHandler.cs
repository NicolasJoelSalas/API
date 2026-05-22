using Application.UseCases.EVENT.Commands;

namespace Application.Interfaces.Handlers.Event
{
    public interface ICreateEventHandler
    {
        Task<string> Handle(CreateEventCommand command);
    }
}