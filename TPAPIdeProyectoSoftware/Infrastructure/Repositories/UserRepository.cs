using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<USER> Query()
        {
            return _context.USER.AsNoTracking().AsQueryable();
        }
        public async Task AddAsync(USER user)
        {
            await _context.USER.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<USER> GetByIdAsync(int id)
        {
            return await _context.USER.FindAsync(id);
        }

        public async Task UpdateAsync(USER user)
        {
            _context.USER.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.USER
                .AsNoTracking()
                .AnyAsync(u => u.Email == email);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.USER.FindAsync(id);

            _context.USER.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}