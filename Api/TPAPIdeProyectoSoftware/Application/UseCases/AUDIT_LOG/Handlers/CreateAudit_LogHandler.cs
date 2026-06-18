using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;

namespace Application.UseCases.Audit_Log.Handlers
{
    public class CreateAudit_LogHandler  : ICreateAudit_LogHandler
    {
        private readonly IAudit_LogRepository _auditLogRepository;
        private readonly IUserRepository _userRepository;

        public CreateAudit_LogHandler(
            IAudit_LogRepository auditLogRepository,
            IUserRepository userRepository)
        {
            _auditLogRepository = auditLogRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateAudit_LogCommand command)
        {
            if (command == null)
                return "Datos inválidos";

            if (command.UserId.HasValue)
            {
                if (command.UserId <= 0)
                    return "El Id del usuario es inválido";

                var user = await _userRepository.GetByIdAsync(command.UserId.Value);

                if (user == null)
                    return "El usuario no existe";
            }

            if (string.IsNullOrWhiteSpace(command.Action))
                return "La acción es obligatoria";

            if (string.IsNullOrWhiteSpace(command.EntityType))
                return "El tipo de entidad es obligatorio";

            if (string.IsNullOrWhiteSpace(command.EntityId))
                return "El id de la entidad es obligatorio";

            if (string.IsNullOrWhiteSpace(command.Details))
                return "Los detalles son obligatorios";

            var auditLog = new Domain.Entities.AUDIT_LOG
            {
                UserId = command.UserId,
                Action = command.Action,
                EntityType = command.EntityType,
                EntityId = command.EntityId,
                Details = command.Details,
                CreatedAt = DateTime.UtcNow
            };

            await _auditLogRepository.AddAsync(auditLog);

            return "OK";
        }
    }
}