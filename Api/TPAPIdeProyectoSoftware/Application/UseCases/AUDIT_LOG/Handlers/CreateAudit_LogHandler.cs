using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;
using Domain.Exceptions;

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
                throw new DataNotFoundException("Datos inválidos");

            if (command.UserId.HasValue)
            {
                if (command.UserId <= 0)
                    throw new MissingDataException("El Id del usuario es inválido");

                var user = await _userRepository.GetByIdAsync(command.UserId.Value);

                if (user == null)
                    throw new DataNotFoundException("El usuario no existe");
            }

            if (string.IsNullOrWhiteSpace(command.Action))
                throw new MissingDataException("La acción es obligatoria");

            if (string.IsNullOrWhiteSpace(command.EntityType))
                throw new MissingDataException("El tipo de entidad es obligatorio");

            if (string.IsNullOrWhiteSpace(command.EntityId))
                throw new MissingDataException("El id de la entidad es obligatorio");

            if (string.IsNullOrWhiteSpace(command.Details))
                throw new MissingDataException("Los detalles son obligatorios");

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