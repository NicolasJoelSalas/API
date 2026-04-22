using Application.DTOs.User;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Queries
{
    public class GetByIdAudit_LogQuery : IGetByIdAudit_LogQuery
    {
        private readonly IAudit_LogRepository _repository;

        public GetByIdAudit_LogQuery(IAudit_LogRepository repository)
        {
            _repository = repository;
        }

        public async Task<Audit_LogResponseDto> GetById(Guid id)
        {
            return await _repository.Query().Where(x => x.Id == id).Select(x => new Audit_LogResponseDto
            {
                UserId = x.UserId,
                Action = x.Action,
                EntityType = x.EntityType,
            }).FirstOrDefaultAsync();
        }
    }
}