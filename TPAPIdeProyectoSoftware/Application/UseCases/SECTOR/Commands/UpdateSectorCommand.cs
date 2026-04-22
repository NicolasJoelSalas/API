using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application
{
    public class UpdateSectorCommand : IUpdateSectorCommand
    {
        private readonly ISectorRepository _repository;

        public UpdateSectorCommand(ISectorRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteUpdateSector(SECTOR sector)
        {
            await _repository.UpdateAsync(sector);
        }
    }
}