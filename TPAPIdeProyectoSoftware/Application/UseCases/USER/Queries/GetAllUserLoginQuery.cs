using Application.DTOs.User;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Queries
{
    public class GetAllUserLoginQuery : IGetAllUserLoginQuery
    {
        private readonly IUserRepository _repository;

        public GetAllUserLoginQuery(IUserRepository repository)
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
                    Email = x.Email,
                    PasswordHash = x.PasswordHash,
                }).ToListAsync();
        }
    }
}
