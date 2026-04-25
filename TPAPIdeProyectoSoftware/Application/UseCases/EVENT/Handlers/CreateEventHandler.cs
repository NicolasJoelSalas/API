using Application.DTOs.Event;
using Application.Interfaces.Command.Event;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Queries.Event;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.UseCases
{
    public class CreateEventHandler : ICreateEventHandler
    {
        private readonly ICreateEventCommand _createEventCommand;
        private readonly INameExistsEventQuery _nameExistsEventQuery;

        public CreateEventHandler(ICreateEventCommand createEventCommand, INameExistsEventQuery nameExistsEventQuery)
        {
            _createEventCommand = createEventCommand;
            _nameExistsEventQuery = nameExistsEventQuery;
        }

        public async Task<string> CreateEventHandle(EventResquestDto dto)
        {
            if (dto == null)
                return "Datos inválidos";
            if (string.IsNullOrWhiteSpace(dto.Name))
                return "El nombre es obligatorio";
            if (dto.EventDate == null)
                return "La fecha del evento es obligatoria";
            if (dto.EventDate < DateTime.UtcNow)
                return "La fecha del evento es invalida. Ingrese una fecha posterior al dia de hoy";
            if (string.IsNullOrWhiteSpace(dto.Venue))
                return "La ubicación del evento es obligatoria";

            var existingEvent = await _nameExistsEventQuery.NameExistsEventHandle(dto.Name);
            if (existingEvent)
                return "El nombre del evento ya existe";

            var @event = new Domain.Entities.EVENT
            {
                Name = dto.Name,
                EventDate = dto.EventDate,
                Venue = dto.Venue,
                Status = dto.Status
            };

            await _createEventCommand.ExecuteCreateEvent(@event);

            return "OK";


        }
    }
}