using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Queries;

namespace Application.UseCases.USER.Handlers
{
    public class GetAllUserHandler : IGetAllUserHandler
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(List<UserResponseDto> users, string message)> Handle(GetAllUserQuery query)
        {
            var users = await _userRepository.GetAllAsync();

            if (users == null || users.Count == 0)
                return (new List<UserResponseDto>(), "No hay usuarios registrados");

            var response = users.Select(user => new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            }).ToList();

            return (response, "OK");
        }
    }
}