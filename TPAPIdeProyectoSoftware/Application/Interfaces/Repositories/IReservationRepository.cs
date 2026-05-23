using Domain.Entities;
using Domain.Enums;
using System;

namespace Application.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<List<RESERVATION>> GetAllAsync();

        Task AddAsync(RESERVATION reservation);

        Task<RESERVATION> GetByIdAsync(Guid id);

        Task UpdateAsync(RESERVATION reservation);

        Task DeleteAsync(RESERVATION reservation);

        Task SaveChangesAsync();

        //Task<List<Guid>> GetReservedSeatIdsAsync(List<Guid> seatIds);

        //Task AddRangeAsync(List<RESERVATION> reservations);

        //Task UpdateStatusAsync(List<Guid> reservationIds, string status);
        //Task UpdateStatusExpiredAsync(Domain.Entities.RESERVATION reservation, string status);
    }
}
