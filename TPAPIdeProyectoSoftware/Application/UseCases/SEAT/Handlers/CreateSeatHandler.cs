using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.User;
using Domain.Entities;
using Domain.Enums;

public class CreateSeatHandler : ICreateSeatHandler
{
    private readonly ICreateSeatCommand _command;
    private readonly IGetByIdSectorQuery _querySector;

    public CreateSeatHandler(ICreateSeatCommand command, IGetByIdSectorQuery querySector)
    {
        _command = command;
       _querySector = querySector;
    }

    public async Task<string> Handle(IdSeatRequestDto dto)
    {
        if (dto == null)
            return "Datos inválidos";

        if (dto.SectorId <= 0)
            return "El Id del sector es obligatorio";

        var sector = await _querySector.GetById(dto.SectorId);

        if (sector == null)
            return "El Sector indicado no existe";

        if (string.IsNullOrWhiteSpace(dto.RowIdentifier))
            return "El identificador de fila es obligatorio";

        if (dto.SeatNumber <= 0 || dto.SeatNumber == null)
            return "El número de asiento es obligatorio";

        

        if (dto.Version <= 0 || dto.Version == null)
            return "La version es obligatorio";

        var seat = new SEAT
        {
            SectorId = dto.SectorId,
            RowIdentifier = dto.RowIdentifier,
            SeatNumber = dto.SeatNumber,
            Status = SeatStatus.Available,
            Version = dto.Version,
        };

        await _command.ExecuteCreateSeat(seat);

        return "OK";
    }
}