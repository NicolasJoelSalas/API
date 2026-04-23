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
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;
        public EventRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<EVENT> Query()
        {
            return _context.EVENT.AsNoTracking().AsQueryable();
        }
        public async Task AddAsync(EVENT eventEntity)
        {
            await _context.EVENT.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<EVENT> GetByIdAsync(int id)
        {
            return await _context.EVENT.FindAsync(id);
        }

        public async Task UpdateAsync(EVENT eventEntity)
        {
            _context.EVENT.Update(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var eventEntity = await _context.EVENT.FindAsync(id);
            if (eventEntity != null)
            {
                _context.EVENT.Remove(eventEntity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.EVENT.AnyAsync(e => e.Name == name);
        }
    }
}