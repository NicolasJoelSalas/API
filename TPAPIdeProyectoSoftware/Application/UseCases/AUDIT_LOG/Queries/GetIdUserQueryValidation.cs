using Application.DTOs.User;
using Application.Interfaces.Queries.Audit_Log;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.AUDIT_LOG.Queries
{
    public class GetIdUserQueryValidation : IGetIdUserQueryValidation
    {
        private readonly IUserRepository _repository;

        public GetIdUserQueryValidation(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDto> GetById(int? id)
        {
            return await _repository.Query().Where(x => x.Id == id).Select(x => new UserResponseDto
            {
                Name = x.Name,
                Email = x.Email
            }).FirstOrDefaultAsync();
        }
    }
}
