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
    public class Audit_LogRepository : IAudit_LogRepository
    {
        private readonly AppDbContext _context;

        public Audit_LogRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<AUDIT_LOG> Query()
        {
            return _context.AUDIT_LOG.AsNoTracking().AsQueryable();
        }
        public async Task AddAsync(AUDIT_LOG audit_log)
        {
            await _context.AUDIT_LOG.AddAsync(audit_log);
            await _context.SaveChangesAsync();
        }
        public async Task<AUDIT_LOG> GetByIdAsync(Guid id)
        {
            return await _context.AUDIT_LOG.FindAsync(id);
        }

        public async Task UpdateAsync(AUDIT_LOG audit_log)
        {
            _context.AUDIT_LOG.Update(audit_log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var audit_log = await _context.AUDIT_LOG.FindAsync(id);

            _context.AUDIT_LOG.Remove(audit_log);
            await _context.SaveChangesAsync();
        }
    }
}
