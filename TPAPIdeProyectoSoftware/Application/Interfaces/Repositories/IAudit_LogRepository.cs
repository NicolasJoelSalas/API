
using Domain.Entities;


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
