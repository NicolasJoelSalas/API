using Application.DTOs;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Exceptions;

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
                throw new DataNotFoundException("Datos inválidos");

            if (id == Guid.Empty)
                throw new MissingDataException("Id inválido");

            var existing = await _auditLogRepository.GetByIdAsync(id);

            if (existing == null)
                throw new DataNotFoundException("Registro de auditoría no encontrado");

            // Validar usuario solo si viene informado
            if (dto.UserId.HasValue)
            {
                if (dto.UserId <= 0)
                    throw new MissingDataException("El Id del usuario es inválido");

                var user = await _userRepository.GetByIdAsync(dto.UserId.Value);

                if (user == null)
                    throw new DataNotFoundException("El usuario no existe");
            }

            if (string.IsNullOrWhiteSpace(dto.Action))
                throw new MissingDataException("La acción es obligatoria");

            if (string.IsNullOrWhiteSpace(dto.EntityType))
                throw new MissingDataException("El tipo de entidad es obligatorio");

            if (string.IsNullOrWhiteSpace(dto.EntityId))
                throw new MissingDataException("El id de entidad es obligatorio");

            if (string.IsNullOrWhiteSpace(dto.Details))
                throw new MissingDataException("Los detalles son obligatorios");

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