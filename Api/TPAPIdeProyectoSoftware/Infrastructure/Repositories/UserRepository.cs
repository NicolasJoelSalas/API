using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<USER?>> GetAllAsync()
        {
            return await _context.USER
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<USER?> GetByIdAsync(int id)
        {
            return await _context.USER
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.USER
                .AsNoTracking()
                .AnyAsync(u => u.Email == email);
        }

        public async Task AddAsync(USER user)
        {
            await _context.USER.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(USER user)
        {
            _context.USER.Update(user);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(USER user)
        {
            _context.USER.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}