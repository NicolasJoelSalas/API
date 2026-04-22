using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Application.UseCases.USER.Queries;
using Domain.Entities;

namespace Application.UseCases
{
    public class UpdateSeatHandler : IUpdateSeatHandler
    {
        private readonly IUpdateSeatCommand _command;
        private readonly IGetByIdSeatQuery _query;
        //private readonly IGetByIdSectorQuery _querySector;

        public UpdateSeatHandler(
            IUpdateSeatCommand command,
            IGetByIdSeatQuery query)
            //IGetByIdSectorQuery _querySector)
        {
            _command = command;
            _query = query;
            //_querySector = _querySector;
        }

        public async Task<string> Handle(Guid id, SeatRequestDto dto)
        {
            //var existing = await _query.GetById(id);

            //if (dto == null)
            //    return "Datos inválidos";

            //if (existing == null)
            //    return "Audit_Log no encontrado";

            //if (dto.UserId <= 0)
            //    return "El Id del usuario es obligatorio";

            //var user = await _queryUser.GetById(dto.UserId);

            //if (user == null)
            //    return "Usuario no existe";

            //if (string.IsNullOrWhiteSpace(dto.Action))
            //    return "La acción es obligatoria";

            //if (string.IsNullOrWhiteSpace(dto.EntityType))
            //    return "El tipo de entidad es obligatoria";

            //if (string.IsNullOrWhiteSpace(dto.EntityId))
            //    return "El id de entidad es obligatoria";

            //var auditLog = new AUDIT_LOG
            //{
            //    Id = id, 
            //    UserId = dto.UserId,
            //    Action = dto.Action,
            //    EntityType = dto.EntityType,
            //    EntityId = dto.EntityId,
            //    Details = dto.Details,
            //    CreatedAt = existing.CreatedAt 
            //};

            //await _command.ExecuteUpdateAudit_Log(auditLog);

            return "Audit_Log actualizado correctamente";
        }
    }
}