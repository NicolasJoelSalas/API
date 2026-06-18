using Application.DTOs;
using Application.DTOs.User;
using Application.UseCases.USER.Queries;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetAllAudit_LogHandler
    {
        Task<(List<Audit_LogResponseDto> AuditLogs, string message)> Handle(GetAllAudit_LogQuery query);
    }
}
