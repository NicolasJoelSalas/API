using Application.Interfaces.Commands.Event;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EVENT.Commands
{
    public class UpdateEventCommand : IUpdateEventCommand
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommand(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task ExecuteUpdateEvent(Domain.Entities.EVENT eventEntity)
        {
            await _eventRepository.UpdateAsync(eventEntity);
        }
    }
}