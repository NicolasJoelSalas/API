using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application
{
    public class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly IUserRepository _repository;

        public UpdateUserCommand(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteUpdateUser(USER user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}