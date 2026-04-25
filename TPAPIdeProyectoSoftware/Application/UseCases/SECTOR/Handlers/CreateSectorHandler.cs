using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handler;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.Event;
using Application.Interfaces.Queries.User;
using Domain.Entities;

public class CreateSectorHandler : ICreateSectorHandler
{
    private readonly ICreateSectorCommand _command;
    private readonly IGetByIdEventQuery _queryEvent;

    public CreateSectorHandler(ICreateSectorCommand command, IGetByIdEventQuery queryEvent)
    {
        _command = command;
        _queryEvent = queryEvent;
       
    }

    public async Task<string> Handle(SectorRequestDto dto)
    {
        if (dto == null)
            return "Datos inválidos";

        var eventt = await _queryEvent.GetById(dto.EventId);

        if (eventt == null)
            return "El Evento no existe";

        if (string.IsNullOrWhiteSpace(dto.Name))
            return "El nombre del sector es obligatorio";

        if (dto.Price.CompareTo(0) <= 0)
            return "Ingrese un precio mayor a 0";

        if (dto.Capacity <= 0)
            return "Ingrese una capacidad mayor a 0";

        var sector = new SECTOR
        {
            EventId = dto.EventId,
            Name = dto.Name,
            Price = dto.Price,
            Capacity = dto.Capacity
        };

        await _command.ExecuteCreateSector(sector);

        return "OK";
    }
}