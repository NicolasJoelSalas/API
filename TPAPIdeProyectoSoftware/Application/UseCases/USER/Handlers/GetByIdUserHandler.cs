using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Handlers
{
    public class GetByIdUserHandler : IGetByIdUserHandler
    {
        private readonly IGetByIdUserHandler _query;

        public GetByIdUserHandler(IGetByIdUserHandler query)
        {
            _query = query;
        }
        public async Task<List<UserResponseDto>> Getbyid(int id)
        {
            return await _query.Getbyid(id);
        }
    }
}
