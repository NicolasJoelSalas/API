using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;

namespace Application.UseCases.USER.Commands
{
    public class DeleteReservationCommand : IDeleteReservationCommand
    {
        private readonly IReservationRepository _repository;

        public DeleteReservationCommand(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteDeleteReservation(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}