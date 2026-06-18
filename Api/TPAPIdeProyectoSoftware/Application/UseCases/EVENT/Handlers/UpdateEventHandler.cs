using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Commands;

namespace Application.UseCases.EVENT.Handlers
{
    public class UpdateEventHandler : IUpdateEventHandler
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<string> Handle(UpdateEventCommand command)
        {
            if (command == null)
                return "Comando inválido";

            if (command.Id <= 0)
                return "Id inválido";

            if (string.IsNullOrWhiteSpace(command.Name))
                return "El nombre es obligatorio";

            if (command.EventDate < DateTime.UtcNow)
                return "La fecha del evento es inválida";

            if (string.IsNullOrWhiteSpace(command.Venue))
                return "El lugar es obligatorio";

            var existingEvent = await _eventRepository.GetByIdAsync(command.Id);

            if (existingEvent == null)
                return "Evento no encontrado";

            existingEvent.Name = command.Name;
            existingEvent.EventDate = command.EventDate;
            existingEvent.Venue = command.Venue;
            existingEvent.Status = command.Status;

            await _eventRepository.UpdateAsync(existingEvent);

            return "Evento actualizado correctamente";
        }
    }
}