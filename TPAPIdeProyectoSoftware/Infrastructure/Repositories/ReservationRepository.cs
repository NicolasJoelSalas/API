using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RESERVATION>> GetAllAsync()
        {
            return await _context.RESERVATION
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RESERVATION?> GetByIdAsync(Guid id)
        {
            return await _context.RESERVATION
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(RESERVATION reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            await _context.RESERVATION.AddAsync(reservation);
        }

        public async Task UpdateAsync(RESERVATION reservation)
        {
            _context.RESERVATION.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<RESERVATION>> GetPendingReservationsAsync()
        {
            return await _context.RESERVATION
                .Where(r => r.Status == "Pending")
                .ToListAsync();
        }

        public async Task<List<RESERVATION>> GetAllByIdAsync(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
                return new List<RESERVATION>();

            return await _context.RESERVATION
                .Where(r => ids.Contains(r.Id))
                .ToListAsync();
        }

        public async Task DeleteAsync(Guid reservationId)
        {
            var reservation = await _context.RESERVATION.FindAsync(reservationId);

            if (reservation != null)
            {
                _context.RESERVATION.Remove(reservation);
            }
        }
        public async Task<List<Guid>> GetReservedSeatIdsAsync(List<Guid> seatIds)
        {
            return await _context.RESERVATION
                .Where(r => seatIds.Contains(r.SeatId) && (r.Status == "Pending" || r.Status == "Reserved"))
                .Select(r => r.SeatId)
                .ToListAsync();
        }
        public async Task AddRangeAsync(List<RESERVATION> reservations)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            await _context.RESERVATION.AddRangeAsync(reservations);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

    }
}