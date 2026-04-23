using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Queries
{
    public class GetAllAudit_LogQuery : IGetAllAudit_LogQuery
    {
        private readonly IAudit_LogRepository _repository;

        public GetAllAudit_LogQuery(IAudit_LogRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<Audit_LogResponseDto>> GetAll()
        {
            return await _repository.Query()
                .OrderBy(x => x.Id)
                .Select(x => new Audit_LogResponseDto
                {
                    UserId = x.UserId,
                    Action = x.Action,
                    EntityType = x.EntityType,
                }).ToListAsync();
        }
    }
}
