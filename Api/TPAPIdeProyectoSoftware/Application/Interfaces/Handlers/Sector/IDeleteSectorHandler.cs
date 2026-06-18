using Application.DTOs.User;
using Application.UseCases;

namespace Application.Interfaces.Handlers
{
    public interface IDeleteSectorHandler
    {
        Task<string> Handle(DeleteSectorCommand command);
    }
}
