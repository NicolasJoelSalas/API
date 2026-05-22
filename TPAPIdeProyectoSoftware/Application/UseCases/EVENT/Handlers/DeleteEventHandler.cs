using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Commands;

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
            if (command == null)
                return "Comando inválido";

            if (command.Id <= 0)
                return "Id inválido";

            var eventEntity = await _eventRepository.GetByIdAsync(command.Id);

            if (eventEntity == null)
                return "Evento no encontrado";

            await _eventRepository.DeleteAsync(eventEntity);

            return "Evento eliminado correctamente";
        }
    }
}