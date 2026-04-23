using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.Audit_Log;
using Application.Interfaces.Queries.User;
using Application.UseCases.AUDIT_LOG.Queries;
using Domain.Entities;

public class CreateAudit_LogHandler : ICreateAudit_LogHandler
{
    private readonly ICreateAudit_LogCommand _command;
    private readonly IGetIdUserQueryValidation _queryUser;

    public CreateAudit_LogHandler(ICreateAudit_LogCommand command, IGetIdUserQueryValidation queryUser)
    {
        _command = command;
        _queryUser = queryUser;
    }

    public async Task<string> Handle(Audit_LogRequestDto dto)
    {
        if (dto == null)
            return "Datos inválidos";

        // 🔹 Validar usuario SOLO si viene informado
        if (dto.UserId.HasValue)
        {
            if (dto.UserId <= 0)
                return "El Id del usuario es inválido";

            var user = await _queryUser.GetById(dto.UserId.Value);

            if (user == null)
                return "El usuario no existe";
        }

        if (string.IsNullOrWhiteSpace(dto.Action))
            return "La acción es obligatoria";

        if (string.IsNullOrWhiteSpace(dto.EntityType))
            return "El tipo de entidad es obligatorio";

        if (string.IsNullOrWhiteSpace(dto.EntityId))
            return "El id de la entidad es obligatorio";

        if (string.IsNullOrWhiteSpace(dto.Details))
            return "Los detalles son obligatorios";

        var Audit_Log = new AUDIT_LOG
        {
            UserId = dto.UserId,
            Action = dto.Action,
            EntityType = dto.EntityType,
            EntityId = dto.EntityId,
            Details = dto.Details,
            CreatedAt = DateTime.UtcNow,
        };

        await _command.ExecuteCreateAudit_Log(Audit_Log);

        return "OK";
    }
}