
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;

namespace Application.UseCases
{
    public class UpdateSeatHandler : IUpdateSeatHandler
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly ISeatRepository _seatRepository;

        public UpdateSeatHandler(ISeatRepository seatRepository, ISectorRepository sectorRepository)
        {
            _seatRepository = seatRepository;
            _sectorRepository = sectorRepository;
        }

        public async Task<string> Handle(UpdateSeatCommand command)
        {
            if (command == null)
                return "Comando inválido";


            if (command.SectorId <= 0)
                return "El Id del sector es obligatorio";

            var existingSector = await _sectorRepository.GetByIdAsync(command.SectorId);

            if (existingSector == null)
                return "Sector no encontrado";

            if (string.IsNullOrWhiteSpace(command.RowIdentifier))
                return "El identificador de fila es obligatorio";

            if (command.SeatNumber <= 0)
                return "El número de asiento es obligatorio";

            if (command.Status == null)
                return "El estado es obligatorio";

            if (command.Version <= 0)
                return "El número de versión es obligatorio";

            var existingseat = await _seatRepository.GetByIdAsync(command.Id);

            if (existingseat == null)
                return "Asiento no encontrado";

            existingseat.SectorId = command.SectorId;   
            existingseat.RowIdentifier = command.RowIdentifier;
            existingseat.SeatNumber = command.SeatNumber;
            existingseat.Status = command.Status;
            existingseat.Version = command.Version;

            await _seatRepository.UpdateAsync(existingseat);

            return "Asiento actualizado correctamente";


        }
    }
    
}