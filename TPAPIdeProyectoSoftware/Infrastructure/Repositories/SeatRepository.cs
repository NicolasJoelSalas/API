using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _context;

        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<SEAT> Query()
        {
            return _context.SEAT.AsNoTracking().AsQueryable();
        }
        public async Task AddAsync(SEAT seat)
        {
            await _context.SEAT.AddAsync(seat);
            await _context.SaveChangesAsync();
        }
        public async Task<SEAT> GetByIdAsync(Guid id)
        {
            return await _context.SEAT.FindAsync(id);
        }

        public async Task UpdateAsync(SEAT seat)
        {
            _context.SEAT.Update(seat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var seat = await _context.SEAT.FindAsync(id);

            _context.SEAT.Remove(seat);
            await _context.SaveChangesAsync();
        }
        public async Task MarkAsSoldByReservationIds(List<Guid> reservationIds)
        {
            await _context.SEAT
                .Where(s => _context.RESERVATION
                    .Where(r => reservationIds.Contains(r.Id))
                    .Select(r => r.SeatId)
                    .Contains(s.Id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.Status, "Sold"));
        }
        public async Task MarkAsAvailableAsync(Guid seatId)
        {
            await _context.SEAT
                .Where(s => s.Id == seatId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.Status, "Available"));
        }
    }
}
