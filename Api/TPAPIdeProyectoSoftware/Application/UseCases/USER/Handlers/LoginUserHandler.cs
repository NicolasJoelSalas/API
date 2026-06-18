using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Handlers
{
    public class LoginUserHandler : ILoginUserHandler
    {
        private readonly IUserRepository _userRepository;

        public LoginUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(bool Success, string Message, int? UserId, string? Username)> Handle(GetAllUserLoginQuery query)
        {

            var listaDeusuarios = await _userRepository.GetAllAsync();
            foreach (var user in listaDeusuarios)
            {
                
                if (user.Name == query.Name && user.PasswordHash == query.Password)
                {
                    return (true, "Login exitoso", user.Id, user.Name);
                }
            }

            return (false, "Credenciales inválidas", null, null);
        }
    }
}

