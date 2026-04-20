using Application.DTOs.User;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Queries
{
    public class GetByIdUserQuery : IGetByIdUserQuery
    {
        private readonly IUserRepository _repository;

        public GetByIdUserQuery(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDto> GetById(int id)
        {
            return await _repository.Query().Where(x => x.Id == id).Select(x => new UserResponseDto
                {
                    Name = x.Name,
                    Email = x.Email
                }).FirstOrDefaultAsync();
        }
    }
}