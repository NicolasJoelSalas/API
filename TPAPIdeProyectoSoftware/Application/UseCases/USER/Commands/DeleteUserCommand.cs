using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;

namespace Application.UseCases.USER.Commands
{
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly IUserRepository _repository;

        public DeleteUserCommand(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteDeleteUser(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}