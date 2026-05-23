using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RESERVATION>> GetAllAsync()
        {
            return await _context.RESERVATION
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RESERVATION?> GetByIdAsync(Guid id)
        {
            return await _context.RESERVATION
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(RESERVATION reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            await _context.RESERVATION.AddAsync(reservation);
        }

        public async Task UpdateAsync(RESERVATION reservation)
        {
            _context.RESERVATION.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RESERVATION reservation)
        {
            _context.RESERVATION.Remove(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}