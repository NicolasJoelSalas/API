using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;
using Domain.Exceptions;

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
                throw new DataNotFoundException("Comando inválido");

            if (command.Id <= 0)
                throw new MissingDataException("Id inválido");

            var sector = await _sectorRepository.GetByIdAsync(command.Id);

            if (sector == null)
                throw new DataNotFoundException("Sector no encontrado");

            await _sectorRepository.DeleteAsync(sector);

            return "Sector eliminado correctamente";

        }
    }
}