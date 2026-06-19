using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Commands;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.UseCases.Event.Handlers
{
    public class CreateEventHandler : ICreateEventHandler
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<string> Handle(CreateEventCommand command)
        {
            if (command == null)
                throw new DataNotFoundException("Datos inválidos");

            if (string.IsNullOrWhiteSpace(command.Name))
                throw new MissingDataException("El nombre es obligatorio");

            if (command.EventDate.Date < DateTime.UtcNow.Date)
                throw new MissingDataException("La fecha del evento es inválida");

            if (string.IsNullOrWhiteSpace(command.Venue))
                throw new MissingDataException("La ubicación es obligatoria");

            var exists = await _eventRepository.NameExistsAsync(command.Name);

            if (exists)
                throw new MissingDataException("El nombre del evento ya existe");

            var eventEntity = new Domain.Entities.EVENT
            {
                Name = command.Name,
                EventDate = command.EventDate,
                Venue = command.Venue,
                Status = EventStatus.Active.ToString()
            };

            await _eventRepository.AddAsync(eventEntity);

            return "OK";
        }
    }
}