using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
            _context.SEAT.Update(seat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SEAT seat)
        {
            _context.SEAT.Remove(seat);
            await _context.SaveChangesAsync();
        }
    }
}