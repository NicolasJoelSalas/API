using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISectorRepository
    {
        Task AddAsync(SECTOR seat);

        Task<List<SECTOR>> GetAllAsync();
        Task<SECTOR> GetByIdAsync(int id);

        Task UpdateAsync(SECTOR seat);

        Task DeleteAsync(SECTOR sector);
    }
}
