using Application.Interfaces.Command.Event;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EVENT.Commands
{
    public class CreateEventCommand : ICreateEventCommand
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommand(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task ExecuteCreateEvent(Domain.Entities.EVENT eventEntity)
        {
            await _eventRepository.AddAsync(eventEntity);
        }
    }
}