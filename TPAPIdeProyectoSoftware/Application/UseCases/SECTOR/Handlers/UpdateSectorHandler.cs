
using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;


namespace Application.UseCases
{
    public class UpdateSectorHandler : IUpdateSectorHandler
    {
        private readonly ISectorRepository _sectorRepository;

        public UpdateSectorHandler(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<string> Handle(UpdateSectorCommand command)
        {
            if (command == null)
                return "Comando inválido";


            if (command.EventId <= 0)
                return "El Id del evento es obligatorio";

            if (string.IsNullOrWhiteSpace(command.Name))
                return "El nombre es obligatorio";

            if (command.Price <= 0)
                return "El precio es obligatorio";

            if (command.Capacity <= 0)
                return "La capacidad es obligatoria";

            var existingsector = await _sectorRepository.GetByIdAsync(command.Id);

            if (existingsector == null)
                return "Sector no encontrado";

            existingsector.EventId = command.EventId;
            existingsector.Name = command.Name;
            existingsector.Price = command.Price;
            existingsector.Capacity = command.Capacity;

            await _sectorRepository.UpdateAsync(existingsector);

            return "Sector actualizado correctamente";
        }
    }
}