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
    public class GetByIdUserHandler : IGetByIdUserHandler
    {
        private readonly IGetByIdUserQuery _query;

        public GetByIdUserHandler(IGetByIdUserQuery query)
        {
            _query = query;
        }
        public async Task<(UserResponseDto users, string message)> Handle(int id)
        {
            var user = await _query.GetById(id);

            if (user == null)
                return (new UserResponseDto(), "No hay usuarios registrados");

            return (user, "OK");
        }
    }
}
