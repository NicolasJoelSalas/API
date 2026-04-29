using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Handlers
{
    public class LoginUserHandler : ILoginUserHandler
    {
        private readonly IGetAllUserLoginQuery _getAllUserLoginQuery;

        public LoginUserHandler(IGetAllUserLoginQuery getAllUserQuery)
        {
            _getAllUserLoginQuery = getAllUserQuery;
        }

        public async Task<(bool Success, string Message, int? UserId, string? Username)> Handle(LoginRequest request)
        {
            //var (users, message) = await _getAllUserHandler.Handle();

            //if (message != "OK")
            //    return (false, message, null, null);

            //var user = users.FirstOrDefault(u =>
            //    u.Name == request.Name &&
            //    u.PasswordHash == request.PasswordHash);



            //if (user == null)
            //    return (false, "Credenciales inválidas", null, null);

            //return (true, "Login exitoso", user.Id, user.Name);
            
            var listaDeusuarios = await _getAllUserLoginQuery.GetAll();
            foreach(var user in listaDeusuarios)
            {
                //return (false, user.PasswordHash, user.Id, user.Name);
                if (user.Name == request.Name && user.PasswordHash == request.PasswordHash)
                {
                    return (true, "Login exitoso", user.Id, user.Name);
                }
            }

            return (false, "Credenciales inválidas", null, null);
        }
    }
}
