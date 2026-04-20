using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Domain.Entities;

namespace Application.UseCases.USER.Handlers
{
    public class UpdateUserHandler : IUpdateUserHandler
    {
        private readonly IUpdateUserCommand _command;
        private readonly IGetByIdUserQuery _query;

        public UpdateUserHandler(
            IUpdateUserCommand command,
            IGetByIdUserQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<string> Handle(int id, UserRequestDto dto)
        {
            var userDto = await _query.GetById(id);

            if (userDto == null)
                return "Usuario no encontrado";

            var user = new Domain.Entities.USER
            {
                Id = id,
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash
            };

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.PasswordHash = dto.PasswordHash;

            await _command.ExecuteUpdateUser(user);

            return "Usuario actualizado correctamente";
        }
    }
}