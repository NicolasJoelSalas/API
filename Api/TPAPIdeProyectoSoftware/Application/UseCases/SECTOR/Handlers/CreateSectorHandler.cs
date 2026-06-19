using Application.Interfaces.Handler;
using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;
using Application.UseCases;
using Domain.Entities;
using Domain.Exceptions;

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
            throw new DataNotFoundException("Datos inválidos");

        if (command.EventId <= 0)
            throw new MissingDataException("Evento inválido");

        if (string.IsNullOrWhiteSpace(command.Name))
            throw new MissingDataException("El nombre del sector es obligatorio");

        if (command.Price <= 0)
            throw new MissingDataException("Ingrese un precio mayor a 0");

        if (command.Capacity <= 0)
            throw new MissingDataException("Ingrese una capacidad mayor a 0");

        var eventExists = await _eventRepository.GetByIdAsync(command.EventId);

        if (eventExists == null)
            throw new DataNotFoundException("El Evento no existe");

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