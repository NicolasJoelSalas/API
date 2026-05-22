using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;

namespace Application.UseCases
{
    public class DeleteSectorHandler : IDeleteSectorHandler
    {
        private readonly ISectorRepository _sectorRepository;

        public DeleteSectorHandler(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<string> Handle(DeleteSectorCommand command)
        {
            if (command == null)
                return "Comando inválido";

            if (command.Id <= 0)
                return "Id inválido";

            var sector = await _sectorRepository.GetByIdAsync(command.Id);

            if (sector == null)
                return "Sector no encontrado";

            await _sectorRepository.DeleteAsync(sector);

            return "Sector eliminado correctamente";

        }
    }
}