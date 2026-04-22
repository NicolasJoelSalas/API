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
    public class SectorRepository : ISectorRepository
    {
        private readonly AppDbContext _context;

        public SectorRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<SECTOR> Query()
        {
            return _context.SECTOR.AsNoTracking().AsQueryable();
        }
        public async Task AddAsync(SECTOR sector)
        {
            await _context.SECTOR.AddAsync(sector);
            await _context.SaveChangesAsync();
        }
        public async Task<SECTOR> GetByIdAsync(int id)
        {
            return await _context.SECTOR.FindAsync(id);
        }

        public async Task UpdateAsync(SECTOR sector)
        {
            _context.SECTOR.Update(sector);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sector = await _context.SECTOR.FindAsync(id);

            _context.SECTOR.Remove(sector);
            await _context.SaveChangesAsync();
        }
    }
}
