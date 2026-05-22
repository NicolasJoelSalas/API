using Application.DTOs;
using Application.DTOs.User;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IAudit_LogRepository
    {
        Task AddAsync(AUDIT_LOG audit);

        Task<AUDIT_LOG> GetByIdAsync(Guid id);

        Task<List<AUDIT_LOG>> GetAllAsync();

        Task UpdateAsync(AUDIT_LOG audit);

        Task DeleteAsync(AUDIT_LOG audit);
    }
}
