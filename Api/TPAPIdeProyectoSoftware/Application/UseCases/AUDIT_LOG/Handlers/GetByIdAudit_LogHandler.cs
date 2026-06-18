using Application.DTOs;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Queries;

namespace Application.UseCases.Audit_Log.Handlers
{
    public class GetByIdAudit_LogHandler : IGetByIdAudit_LogHandler
    {
        private readonly IAudit_LogRepository _auditLogRepository;

        public GetByIdAudit_LogHandler(IAudit_LogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<(Audit_LogResponseDto AuditLog, string message)> Handle(GetByIdAudit_LogQuery query)
        {
            if (query == null)
                return (new Audit_LogResponseDto(), "Query inválida");

            if (query.Id == Guid.Empty)
                return (new Audit_LogResponseDto(), "Id inválido");

            var auditLog = await _auditLogRepository.GetByIdAsync(query.Id);

            if (auditLog == null)
                return (new Audit_LogResponseDto(), "Registro de auditoría no encontrado");

            return (new Audit_LogResponseDto
            {
                UserId = auditLog.UserId,
                Action = auditLog.Action,
                EntityType = auditLog.EntityType,
                EntityId = auditLog.EntityId,
                Details = auditLog.Details,
                CreatedAt = auditLog.CreatedAt
            }, "OK");
        }
    }
}