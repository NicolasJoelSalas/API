using Domain.Entities;
using Domain.Enums;
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

        Task<List<Guid>> GetReservedSeatIdsAsync(List<Guid> seatIds);
        Task AddRangeAsync(List<RESERVATION> reservations);

        Task UpdateStatusAsync(List<Guid> reservationIds, string status);
        Task UpdateStatusExpiredAsync(Domain.Entities.RESERVATION reservation, string status);
    }
}
