using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;
using Domain.Entities;
using Domain.Exceptions;

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
                throw new DataNotFoundException("Datos inválidos");

            if (string.IsNullOrWhiteSpace(command.Name))
                throw new MissingDataException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(command.Email))
                throw new MissingDataException("El email es obligatorio");

            if (string.IsNullOrWhiteSpace(command.PasswordHash))
                throw new MissingDataException("La contraseña es obligatoria");

            var exists = await _userRepository.EmailExistsAsync(command.Email);

            if (exists)
                throw new DataNotFoundException("El email ya está registrado, use otro");

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