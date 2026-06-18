using Application.DTOs;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases.Audit_Log.Handlers
{
    public class UpdateAudit_LogHandler : IUpdateAudit_LogHandler
    {
        private readonly IAudit_LogRepository _auditLogRepository;
        private readonly IUserRepository _userRepository;

        public UpdateAudit_LogHandler(
            IAudit_LogRepository auditLogRepository,
            IUserRepository userRepository)
        {
            _auditLogRepository = auditLogRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(Guid id, Audit_LogRequestDto dto)
        {
            if (dto == null)
                return "Datos inválidos";

            if (id == Guid.Empty)
                return "Id inválido";

            var existing = await _auditLogRepository.GetByIdAsync(id);

            if (existing == null)
                return "Registro de auditoría no encontrado";

            // Validar usuario solo si viene informado
            if (dto.UserId.HasValue)
            {
                if (dto.UserId <= 0)
                    return "El Id del usuario es inválido";

                var user = await _userRepository.GetByIdAsync(dto.UserId.Value);

                if (user == null)
                    return "El usuario no existe";
            }

            if (string.IsNullOrWhiteSpace(dto.Action))
                return "La acción es obligatoria";

            if (string.IsNullOrWhiteSpace(dto.EntityType))
                return "El tipo de entidad es obligatorio";

            if (string.IsNullOrWhiteSpace(dto.EntityId))
                return "El id de entidad es obligatorio";

            if (string.IsNullOrWhiteSpace(dto.Details))
                return "Los detalles son obligatorios";

            // Modificar entidad existente
            existing.UserId = dto.UserId;
            existing.Action = dto.Action;
            existing.EntityType = dto.EntityType;
            existing.EntityId = dto.EntityId;
            existing.Details = dto.Details;

            // No tocar CreatedAt
            await _auditLogRepository.UpdateAsync(existing);

            return "Registro de auditoría actualizado correctamente";
        }
    }
}