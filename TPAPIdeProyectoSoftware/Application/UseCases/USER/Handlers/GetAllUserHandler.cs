using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Handlers
{
    public class GetAllUserHandler : IGetAllUserHandler
    {
        private readonly IGetAllUserQuery _query;

        public GetAllUserHandler(IGetAllUserQuery query)
        {
            _query = query;
        }
        public async Task<(List<UserResponseDto> users, string message)> Handle()
        {
            var users = await _query.GetAll();

            if (users == null || users.Count == 0)
                return (new List<UserResponseDto>(), "No hay usuarios registrados");

            return (users, "OK");
        }
    }
}
