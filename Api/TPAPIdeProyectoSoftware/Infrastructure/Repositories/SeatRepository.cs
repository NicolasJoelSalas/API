using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Net.NetworkInformation;

namespace Infrastructure.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SEAT>> GetAllAsync()
        {
            return await _context.SEAT
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SEAT?> GetByIdAsync(Guid id)
        {
            return await _context.SEAT
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(SEAT seat)
        {
            await _context.SEAT.AddAsync(seat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SEAT seat)
        {
            try
            {

                _context.SEAT.Update(seat);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                foreach (var entry in _context.ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }

                throw;
            }
        }

        public async Task DeleteAsync(SEAT seat)
        {
            _context.SEAT.Remove(seat);
            await _context.SaveChangesAsync();
        }
        public async Task<List<SEAT>> GetSeatBySectorId(int sectorId)
        {
            return await _context.SEAT
                .AsNoTracking()
                .Where(s => s.SectorId == sectorId)
                .ToListAsync();
        }
        public async Task UpdateStatusAsync(Guid seatId, string status)
        {
            await _context.SEAT
                .Where(s => s.Id == seatId)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(se => se.Status, status));
        }
        public async Task IncrementVersionAsync(Guid seatId)
        {
            await _context.SEAT
                .Where(s => s.Id == seatId)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(se => se.Version, se => se.Version + 1));
        }
        public async Task<List<Guid>> GetReservedSeatIdsAsync(List<Guid> seatIds)
        {
            return await _context.SEAT
                .Where(s => seatIds.Contains(s.Id) && (s.Status == "Reserved" || s.Status == "Sold"))
                .Select(s => s.Id)
                .ToListAsync();
        }
        public async Task<bool> ReserveSeatAsync(Guid seatId, int expectedVersion)
        {
            var rows = await _context.SEAT
                .Where(s => s.Id == seatId && s.Version == expectedVersion)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(x => x.Status, "Reserved")
                     .SetProperty(x => x.Version, x => x.Version + 1));

            return rows > 0;
        }
    }
}