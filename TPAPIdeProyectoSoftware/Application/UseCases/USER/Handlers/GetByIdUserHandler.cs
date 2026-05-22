using Application.DTOs.Event;
using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Queries;
using Application.UseCases.USER.Queries;

namespace Application.UseCases.USER.Handlers
{
    public class GetByIdUserHandler : IGetByIdUserHandler
    {
        private readonly IUserRepository _userRepository;

        public GetByIdUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(UserResponseDto? user, string message)> Handle(GetByIdUserQuery query)
        {
            if (query == null)
                return (new UserResponseDto(), "Query inválida");

            if (query.Id <= 0)
                return (null, "Id inválido");

            var userEntity = await _userRepository.GetByIdAsync(query.Id);

            if (userEntity == null)
                return (null, "Usuario no encontrado");

            return (new UserResponseDto
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email
            }, "OK");
        }
    }
}