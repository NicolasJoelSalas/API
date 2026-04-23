using Application.DTOs.Event;
using Application.Interfaces.Commands.Event;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Queries.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.UseCases.EVENT.Commands
{
    public class UpdateEventHandler : IUpdateEventHandler
    {
        private readonly IUpdateEventCommand _updateEventCommand;
        private readonly IGetAllByIdEventQuery _getAllByIdEventQuery;

        public UpdateEventHandler(IUpdateEventCommand updateEventCommand, IGetAllByIdEventQuery getAllByIdEventQuery)
        {
            _updateEventCommand = updateEventCommand;
            _getAllByIdEventQuery = getAllByIdEventQuery;
        }

        public async Task<string> UpdateEventHandle(int id, EventResquestDto dto)
        {

            var existingEvent = await _getAllByIdEventQuery.GetById(id);

            if (existingEvent == null)
                return "Evento no encontrado";

            var updatedEvent = new Domain.Entities.EVENT
            {
                Id = id,
                Name = dto.Name,
                EventDate = dto.EventDate,
                Venue = dto.Venue,
                Status = dto.Status
            };

            updatedEvent.Name = dto.Name;
            updatedEvent.EventDate = dto.EventDate;
            updatedEvent.Venue = dto.Venue;
            updatedEvent.Status = dto.Status;

            await _updateEventCommand.ExecuteUpdateEvent(updatedEvent);

            return "Evento actualizado correctamente";
        }

    }
}