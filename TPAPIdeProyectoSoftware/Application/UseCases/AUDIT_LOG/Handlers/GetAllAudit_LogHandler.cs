using Application.DTOs;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Queries;

namespace Application.UseCases.Audit_Log.Handlers
{
    public class GetAllAudit_LogHandler : IGetAllAudit_LogHandler
    {
        private readonly IAudit_LogRepository _auditLogRepository;

        public GetAllAudit_LogHandler(IAudit_LogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<(List<Audit_LogResponseDto> AuditLogs, string message)> Handle(GetAllAudit_LogQuery query)
        {
            var auditLogs = await _auditLogRepository.GetAllAsync();

            if (auditLogs == null || !auditLogs.Any())
                return (new List<Audit_LogResponseDto>(), "No hay registros de auditoría");

            var auditLogDtos = auditLogs.Select(auditLog => new Audit_LogResponseDto
            {
                UserId = auditLog.UserId,
                Action = auditLog.Action,
                EntityType = auditLog.EntityType,
                EntityId = auditLog.EntityId,
                Details = auditLog.Details,
                CreatedAt = auditLog.CreatedAt
            }).ToList();

            return (auditLogDtos, "OK");
        }
    }
}