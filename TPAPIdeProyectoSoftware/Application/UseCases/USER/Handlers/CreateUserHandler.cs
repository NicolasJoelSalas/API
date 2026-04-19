using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application
{
    public class CreateUserHandler : ICreateUserHandler
    {
        private readonly ICreateUserCommand _command;

        public CreateUserHandler(ICreateUserCommand command)
        {
            _command = command;
        }

        public async Task Handle(UserRequestDto dto)
        {
            Console.WriteLine("Handler ejecutado");

            var user = new USER
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash
            };

            await _command.ExecuteCreateUser(user);
        }

    }
}