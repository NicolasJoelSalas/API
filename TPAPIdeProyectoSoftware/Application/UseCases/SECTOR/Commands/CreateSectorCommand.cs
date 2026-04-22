using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CreateSectorCommand : ICreateSectorCommand
    {
        private readonly ISectorRepository _repository;

        public CreateSectorCommand(ISectorRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteCreateSector(Domain.Entities.SECTOR sector)
        {
            await _repository.AddAsync(sector);
        }

    }
}