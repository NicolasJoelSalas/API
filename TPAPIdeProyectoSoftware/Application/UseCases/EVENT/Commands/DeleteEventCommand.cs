using Application.Interfaces.Commands.Event;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EVENT.Commands
{
    public class DeleteEventCommand : IDeleteEventCommand
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommand(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task ExecuteDeleteEvent(int id)
        {
            await _eventRepository.DeleteAsync(id);
        }
    }
}