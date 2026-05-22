using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;
using Domain.Entities;

namespace Application.UseCases.USER.Handlers
{
    public class CreateUserHandler : ICreateUserHandler
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateUserCommand command)
        {
            if (command == null)
                return "Datos inválidos";

            if (string.IsNullOrWhiteSpace(command.Name))
                return "El nombre es obligatorio";

            if (string.IsNullOrWhiteSpace(command.Email))
                return "El email es obligatorio";

            if (string.IsNullOrWhiteSpace(command.PasswordHash))
                return "La contraseña es obligatoria";

            var exists = await _userRepository.EmailExistsAsync(command.Email);

            if (exists)
                return "El email ya está registrado, use otro";

            var user = new Domain.Entities.USER
            {
                Name = command.Name,
                Email = command.Email,
                PasswordHash = command.PasswordHash
            };

            await _userRepository.AddAsync(user);

            return "OK";
        }
    }
}