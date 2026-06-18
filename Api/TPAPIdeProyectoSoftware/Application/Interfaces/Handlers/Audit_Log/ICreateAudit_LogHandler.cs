using Application.DTOs;
using Application.DTOs.User;
using Application.UseCases.USER.Commands;


namespace Application.Interfaces.Handlers.User
{
    public interface ICreateAudit_LogHandler
    {
        Task<string> Handle(CreateAudit_LogCommand command);
    }
}
