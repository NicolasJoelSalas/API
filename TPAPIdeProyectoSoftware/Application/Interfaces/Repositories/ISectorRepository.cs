using Domain.Entities;
using System;

namespace Application.Interfaces.Repositories
{
    public interface ISectorRepository
    {
        Task AddAsync(SECTOR seat);

        Task<List<SECTOR>> GetAllAsync();
        Task<SECTOR> GetByIdAsync(int id);

        Task UpdateAsync(SECTOR seat);

        Task DeleteAsync(SECTOR sector);

        Task<List<SECTOR>> GetSectorsByEventId(int eventId);
    }
}
