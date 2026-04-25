using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.Audit_Log;
using Application.Interfaces.Queries.User;
using Domain.Entities;

namespace Application.UseCases
{
    public class UpdateAudit_LogHandler : IUpdateAudit_LogHandler
    {
        private readonly IUpdateAudit_LogCommand _command;
        private readonly IGetByIdAudit_LogQuery _query;
        private readonly IGetIdUserQueryValidation _queryUser;

        public UpdateAudit_LogHandler(
            IUpdateAudit_LogCommand command,
            IGetByIdAudit_LogQuery query,
            IGetIdUserQueryValidation queryUser)
        {
            _command = command;
            _query = query;
            _queryUser = queryUser;
        }

        public async Task<string> Handle(Guid id, Audit_LogRequestDto dto)
        {
            var existing = await _query.GetById(id);

            if (dto == null)
                return "Datos inválidos";

            if (existing == null)
                return "Registro de auditoria no encontrado";

            if (dto.UserId <= 0)
                return "El Id del usuario es obligatorio";

            var user = await _queryUser.GetById(dto.UserId);

            if (user == null)
                return "Usuario no existe";

            if (string.IsNullOrWhiteSpace(dto.Action))
                return "La acción es obligatoria";

            if (string.IsNullOrWhiteSpace(dto.EntityType))
                return "El tipo de entidad es obligatoria";

            if (string.IsNullOrWhiteSpace(dto.EntityId))
                return "El id de entidad es obligatoria";

            var auditLog = new Domain.Entities.AUDIT_LOG
            {
                Id = id, 
                UserId = dto.UserId,
                Action = dto.Action,
                EntityType = dto.EntityType,
                EntityId = dto.EntityId,
                Details = dto.Details,
                CreatedAt = existing.CreatedAt 
            };

            await _command.ExecuteUpdateAudit_Log(auditLog);

            return "Registro de auditoria actualizado correctamente";
        }
    }
}