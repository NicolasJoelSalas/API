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

        Task DeleteAsync(Guid reservationId);

        Task<List<RESERVATION>> GetPendingReservationsAsync();

        Task<List<Guid>> GetReservedSeatIdsAsync(List<Guid> seatIds);

        Task<List<RESERVATION>> GetAllByIdAsync(List<Guid> ids);

        Task AddRangeAsync(List<RESERVATION> reservations);

        Task SaveChangesAsync();

        Task<List<Guid>> AddAllAsync(List<RESERVATION> reservations);
        Task UpdateStatusAsync(Guid reservationId, string status);
        

    }
}
