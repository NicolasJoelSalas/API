using Application.UseCases.EVENT.Commands;

namespace Application.Interfaces.Handlers.Event
{
    public interface IDeleteEventHandler
    {
        Task<string> Handle(DeleteEventCommand command);
    }
}