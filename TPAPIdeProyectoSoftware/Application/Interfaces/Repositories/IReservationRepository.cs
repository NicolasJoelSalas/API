using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        IQueryable<RESERVATION> Query();
        Task AddAsync(RESERVATION reser);

        Task<RESERVATION> GetByIdAsync(Guid id);

        Task UpdateAsync(RESERVATION user);

        Task DeleteAsync(Guid id);
    }
}
