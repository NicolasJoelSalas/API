using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SectorRepository : ISectorRepository
    {
        private readonly AppDbContext _context;

        public SectorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SECTOR>> GetAllAsync()
        {
            return await _context.SECTOR
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SECTOR?> GetByIdAsync(int id)
        {
            return await _context.SECTOR
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(SECTOR sector)
        {
            await _context.SECTOR.AddAsync(sector);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SECTOR sector)
        {
            _context.SECTOR.Update(sector);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SECTOR sector)
        {
            _context.SECTOR.Remove(sector);
            await _context.SaveChangesAsync();
        }
    }
}