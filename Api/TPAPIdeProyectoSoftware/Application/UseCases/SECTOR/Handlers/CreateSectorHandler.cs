using Application.Interfaces.Handler;
using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;
using Application.UseCases;
using Domain.Entities;

public class CreateSectorHandler : ICreateSectorHandler
{
    private readonly IEventRepository _eventRepository;
    private readonly ISectorRepository _sectorRepository;

    public CreateSectorHandler(
        IEventRepository eventRepository,
        ISectorRepository sectorRepository)
    {
        _eventRepository = eventRepository;
        _sectorRepository = sectorRepository;
    }

    public async Task<string> Handle(CreateSectorCommand command)
    {
        if (command == null)
            return "Datos inválidos";

        if (command.EventId <= 0)
            return "Evento inválido";

        if (string.IsNullOrWhiteSpace(command.Name))
            return "El nombre del sector es obligatorio";

        if (command.Price <= 0)
            return "Ingrese un precio mayor a 0";

        if (command.Capacity <= 0)
            return "Ingrese una capacidad mayor a 0";

        var eventExists = await _eventRepository.GetByIdAsync(command.EventId);

        if (eventExists == null)
            return "El Evento no existe";

        var sector = new SECTOR
        {
            EventId = command.EventId,
            Name = command.Name,
            Price = command.Price,
            Capacity = command.Capacity
        };

        await _sectorRepository.AddAsync(sector);

        return "OK";
    }
}