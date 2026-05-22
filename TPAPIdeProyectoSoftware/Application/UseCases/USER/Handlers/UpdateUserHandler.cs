using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Commands;
using Domain.Entities;

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
                return "Comando inválido";

            if (string.IsNullOrWhiteSpace(command.Name))
                return "El nombre es obligatorio";

            if (string.IsNullOrWhiteSpace(command.Email))
                return "El email es obligatorio";

            var existingUser = await _userRepository.GetByIdAsync(command.Id);

            if (existingUser == null)
                return "Usuario no encontrado";

            existingUser.Name = command.Name;
            existingUser.Email = command.Email;
            existingUser.PasswordHash = command.PasswordHash;

            await _userRepository.UpdateAsync(existingUser);

            return "Usuario actualizado correctamente";
        }
    }
}