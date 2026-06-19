
using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;
using Domain.Exceptions;


namespace Application.UseCases
{
    public class UpdateSectorHandler : IUpdateSectorHandler
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly IEventRepository _eventRepository;

        public UpdateSectorHandler(ISectorRepository sectorRepository, IEventRepository eventRepository)
        {
            _sectorRepository = sectorRepository;
            _eventRepository = eventRepository;
        }

        public async Task<string> Handle(UpdateSectorCommand command)
        {
            if (command == null)
                throw new DataNotFoundException("Comando inválido");


            if (command.EventId <= 0)
                throw new MissingDataException("El Id del evento es obligatorio");

            var existingEvent = await _eventRepository.GetByIdAsync(command.EventId);

            if (existingEvent == null)
                throw new DataNotFoundException("Evento no encontrado");

            if (string.IsNullOrWhiteSpace(command.Name))
                throw new MissingDataException("El nombre es obligatorio");

            if (command.Price <= 0)
                throw new MissingDataException("El precio es obligatorio");

            if (command.Capacity <= 0)
                throw new MissingDataException("La capacidad es obligatoria");


            var existingsector = await _sectorRepository.GetByIdAsync(command.Id);

            if (existingsector == null)
                throw new DataNotFoundException("Sector no encontrado");

            existingsector.EventId = command.EventId;
            existingsector.Name = command.Name;
            existingsector.Price = command.Price;
            existingsector.Capacity = command.Capacity;

            await _sectorRepository.UpdateAsync(existingsector);

            return "Sector actualizado correctamente";
        }
    }
}