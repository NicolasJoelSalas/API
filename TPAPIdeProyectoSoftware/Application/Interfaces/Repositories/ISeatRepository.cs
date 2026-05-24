using Domain.Entities;
using System;

namespace Application.Interfaces.Repositories
{
    public interface ISeatRepository
    {
        Task<List<SEAT>> GetAllAsync();
        Task AddAsync(SEAT seat);

        Task<SEAT> GetByIdAsync(Guid id);

        Task UpdateAsync(SEAT seat);

        Task DeleteAsync(SEAT seat);

        Task<List<SEAT>> GetSeatBySectorId(int sectorId);

        Task UpdateStatusAsync(Guid seatId, string status);

        Task IncrementVersionAsync(Guid seatId);

        Task<List<Guid>> GetReservedSeatIdsAsync(List<Guid> seatIds);
        //Task MarkAsSoldByReservationIds(List<Guid> reservationIds);
        //Task MarkAsAvailableAsync(Guid seatId);
    }
}
