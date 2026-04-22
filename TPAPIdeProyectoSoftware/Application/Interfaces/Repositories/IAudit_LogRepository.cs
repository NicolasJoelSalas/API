using Application.DTOs.User;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IAudit_LogRepository
    {
        IQueryable<AUDIT_LOG> Query();
        Task AddAsync(AUDIT_LOG user);

        Task<AUDIT_LOG> GetByIdAsync(Guid id);

        Task UpdateAsync(AUDIT_LOG user);

        Task DeleteAsync(Guid id);
    }
}
