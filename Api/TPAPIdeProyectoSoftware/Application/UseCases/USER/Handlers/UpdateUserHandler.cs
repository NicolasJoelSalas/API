using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Commands;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.UseCases.USER.Handlers
{
    public class UpdateUserHandler : IUpdateUserHandler
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(UpdateUserCommand command)
        {
            if (command == null)
                throw new DataNotFoundException("Comando inválido");

            if (string.IsNullOrWhiteSpace(command.Name))
                throw new MissingDataException("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(command.Email))
                throw new MissingDataException("El email es obligatorio");

            var existingUser = await _userRepository.GetByIdAsync(command.Id);
            var exists = await _userRepository.EmailExistsAsync(command.Email);

            if (existingUser == null)
                throw new DataNotFoundException("Usuario no encontrado");

            if (exists && existingUser.Email != command.Email)
                throw new DataNotFoundException("El email ya existe");

            existingUser.Name = command.Name;
            existingUser.Email = command.Email;
            existingUser.PasswordHash = command.PasswordHash;

            await _userRepository.UpdateAsync(existingUser);

            return "Usuario actualizado correctamente";
        }
    }
}