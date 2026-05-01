using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<RESERVATION> Query()
        {
            return _context.RESERVATION.AsNoTracking().AsQueryable();
        }
        public async Task AddAsync(RESERVATION reser)
        {
            await _context.RESERVATION.AddAsync(reser);
            await _context.SaveChangesAsync();
        }
        public async Task<RESERVATION> GetByIdAsync(Guid id)
        {
            return await _context.RESERVATION.FindAsync(id);
        }

        public async Task UpdateAsync(RESERVATION reser)
        {
            _context.RESERVATION.Update(reser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var reser = await _context.RESERVATION.FindAsync(id);

            _context.RESERVATION.Remove(reser);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Guid>> GetReservedSeatIdsAsync(List<Guid> seatIds)
        {
            return await _context.RESERVATION
                .Where(r => seatIds.Contains(r.SeatId))
                .Select(r => r.SeatId)
                .ToListAsync();
        }

        public async Task AddRangeAsync(List<RESERVATION> reservations)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            await _context.RESERVATION.AddRangeAsync(reservations);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        public async Task UpdateStatusAsync(List<Guid> reservationIds, string status)
        {
            var rows = await _context.RESERVATION
                .Where(r => reservationIds.Contains(r.Id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.Status, status)
                );

            Console.WriteLine($"ROWS UPDATED: {rows}");
        }
    }
}
