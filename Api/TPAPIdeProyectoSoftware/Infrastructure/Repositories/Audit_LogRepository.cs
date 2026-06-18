using Application.DTOs;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Audit_LogRepository : IAudit_LogRepository
    {
        private readonly AppDbContext _context;

        public Audit_LogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AUDIT_LOG?>> GetAllAsync()
        {
            return await _context.AUDIT_LOG
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AUDIT_LOG?> GetByIdAsync(Guid id)
        {
            return await _context.AUDIT_LOG
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(AUDIT_LOG auditLog)
        {
            await _context.AUDIT_LOG.AddAsync(auditLog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AUDIT_LOG auditLog)
        {
            _context.AUDIT_LOG.Update(auditLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AUDIT_LOG auditLog)
        {
            _context.AUDIT_LOG.Remove(auditLog);
            await _context.SaveChangesAsync();
        }

    }
}