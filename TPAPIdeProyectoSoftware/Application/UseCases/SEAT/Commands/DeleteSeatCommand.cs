using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;

namespace Application.UseCases.USER.Commands
{
    public class DeleteSeatCommand : IDeleteSeatCommand
    {
        private readonly ISeatRepository _repository;

        public DeleteSeatCommand(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteDeleteSeat(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}