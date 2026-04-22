using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Domain.Entities;

public class CreateSeatHandler : ICreateSeatHandler
{
    private readonly ICreateSeatCommand _command;
    //private readonly IGetByIdSectorQuery _querySector;

    public CreateSeatHandler(ICreateSeatCommand command)//, IGetByIdSectorQuery _querySector)
    {
        _command = command;
       // _querySeat = querySeat;
    }

    public async Task<string> Handle(SeatRequestDto dto)
    {
        //if (dto == null)
        //    return "Datos inválidos";
          
        //if (dto.UserId <= 0)
        //    return "El Id del usuario es obligatorio";

        //var user = await _queryUser.GetById(dto.UserId);

        //if (user == null)
        //    return "Usuario no existe";

        //if (string.IsNullOrWhiteSpace(dto.Action))
        //    return "La accion es obligatorio";

        //if (string.IsNullOrWhiteSpace(dto.EntityType))
        //    return "El tipo de identidad es obligatoria";

        //if (string.IsNullOrWhiteSpace(dto.EntityId))
        //    return "El id de identidad es obligatoria";

        //if (string.IsNullOrWhiteSpace(dto.Details))
            //return "Los detalles son obligatorio";

        //var seat = new SEAT
        //{
        //    UserId = dto.UserId,
        //    SeatId = dto.SeatId,
        //    Status = dto.Status,
        //    ReservedAt = dto.ReservedAt=DateTime.UtcNow,
        //    ExpiresAt = dto.ExpiresAt,
        //};

        //await _command.ExecuteCreateSeat(seat);

        return "OK";
    }
}