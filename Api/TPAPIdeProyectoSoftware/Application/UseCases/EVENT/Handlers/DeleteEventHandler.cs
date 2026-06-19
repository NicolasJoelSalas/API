using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Commands;
using Domain.Exceptions;

namespace Application.UseCases.EVENT.Handlers
{
    public class DeleteEventHandler : IDeleteEventHandler
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<string> Handle(DeleteEventCommand command)
        {

            if (command.Id <= 0)
                throw new MissingDataException("Id inválido");

            var eventEntity = await _eventRepository.GetByIdAsync(command.Id);

            if (eventEntity == null)
                throw new DataNotFoundException("Evento no encontrado");

            await _eventRepository.DeleteAsync(eventEntity);

            return "Evento eliminado correctamente";


        }
    }
}