using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;
using Domain.Exceptions;

namespace Application.UseCases.Audit_Log.Handlers
{
    public class DeleteAudit_LogHandler : IDeleteAudit_LogHandler
    {
        private readonly IAudit_LogRepository _auditLogRepository;

        public DeleteAudit_LogHandler(IAudit_LogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<string> Handle(DeleteAudit_LogCommand command)
        {
            if (command == null)
                throw new DataNotFoundException("Comando inválido");

            if (command.Id == Guid.Empty)
                throw new MissingDataException("Id inválido");

            var auditLog = await _auditLogRepository.GetByIdAsync(command.Id);

            if (auditLog == null)
                throw new DataNotFoundException("Registro de auditoría no encontrado");

            await _auditLogRepository.DeleteAsync(auditLog);

            return "Registro de auditoría eliminado correctamente";
        }
    }
}