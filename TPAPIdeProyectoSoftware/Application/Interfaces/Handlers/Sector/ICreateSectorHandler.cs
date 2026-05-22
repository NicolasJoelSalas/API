using Application.UseCases;

namespace Application.Interfaces.Handler
{
    public interface ICreateSectorHandler
    {
        Task<string> Handle(CreateSectorCommand command);
    }
}
