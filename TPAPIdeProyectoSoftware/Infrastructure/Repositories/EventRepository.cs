using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<EVENT>> GetAllAsync()
        {
            return await _context.EVENT
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<EVENT?> GetByIdAsync(int id)
        {
            return await _context.EVENT.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.EVENT.AsNoTracking().AnyAsync(e => e.Name == name);
        }


        public async Task AddAsync(EVENT eventEntity)
        {
            await _context.EVENT.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EVENT eventEntity)
        {
            _context.EVENT.Update(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EVENT eventEntity)
        {
            _context.EVENT.Remove(eventEntity);
            await _context.SaveChangesAsync();
        }

    }
}