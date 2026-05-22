using Application.UseCases.EVENT.Queries;

namespace Application.Interfaces.Handlers.Event
{
    public interface INameExistsEventHandler
    {
        Task<bool> Handle(NameExistsEventQuery query);
    }
}
