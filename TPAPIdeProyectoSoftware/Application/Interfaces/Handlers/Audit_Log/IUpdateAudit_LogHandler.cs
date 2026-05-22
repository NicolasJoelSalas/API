using Application.DTOs;

namespace Application.Interfaces.Handlers.User
{
    public interface IUpdateAudit_LogHandler
    {
        Task<string> Handle(Guid id, Audit_LogRequestDto dto);
    }
}
