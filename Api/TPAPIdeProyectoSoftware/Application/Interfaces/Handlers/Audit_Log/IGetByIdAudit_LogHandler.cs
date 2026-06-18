using Application.DTOs;
using Application.DTOs.User;
using Application.UseCases.USER.Queries;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetByIdAudit_LogHandler
    {
        Task<(Audit_LogResponseDto AuditLog, string message)> Handle(GetByIdAudit_LogQuery query);

    }
}
