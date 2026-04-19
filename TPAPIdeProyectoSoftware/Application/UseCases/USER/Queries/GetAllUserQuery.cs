using Application.DTOs.User;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Queries
{
    public class GetAllUserQuery : IGetAllUserQuery
    {
        private readonly IUserRepository _repository;

        public GetAllUserQuery(IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<UserResponseDto>> GetAll()
        {
            return await _repository.Query()
                .OrderBy(x => x.Name)
                .Select(x => new UserResponseDto
                {
                    Name = x.Name,
                    Email = x.Email
                }).ToListAsync();
        }
    }
}
