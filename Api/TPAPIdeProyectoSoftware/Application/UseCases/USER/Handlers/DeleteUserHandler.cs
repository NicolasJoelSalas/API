using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Commands;
using Application.UseCases.USER.Commands;
using Domain.Exceptions;

namespace Application.UseCases.USER.Handlers
{
    public class DeleteUserHandler : IDeleteUserHandler
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(DeleteUserCommand command)
        {
            if (command == null)
                throw new DataNotFoundException("Comando inválido");

            if (command.Id <= 0)
                throw new MissingDataException("Id inválido");

            var user = await _userRepository.GetByIdAsync(command.Id);

            if (user == null)
                throw new DataNotFoundException("Usuario no encontrado");

            await _userRepository.DeleteAsync(user);

            return "Usuario eliminado correctamente";

        }

    
    }
}