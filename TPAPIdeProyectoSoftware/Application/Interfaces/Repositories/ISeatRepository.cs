using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISeatRepository
    {
        IQueryable<SEAT> Query();
        Task AddAsync(SEAT seat);

        Task<SEAT> GetByIdAsync(Guid id);

        Task UpdateAsync(SEAT seat);

        Task DeleteAsync(Guid id);
    }
}
