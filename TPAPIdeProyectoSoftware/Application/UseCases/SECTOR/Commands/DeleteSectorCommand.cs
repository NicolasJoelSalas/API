using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;

namespace Application.UseCases
{
    public class DeleteSectorCommand : IDeleteSectorCommand
    {
        private readonly ISectorRepository _repository;

        public DeleteSectorCommand(ISectorRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteDeleteSector(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}