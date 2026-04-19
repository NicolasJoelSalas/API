using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IUserRepository _repository;

        public CreateUserCommand(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteCreateUser(Domain.Entities.USER user)
        {
            Console.WriteLine("Command ejecutado");
            await _repository.AddAsync(user);
        }

    }
}