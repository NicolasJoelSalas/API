using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<USER> USER { get; set; }
        public DbSet<EVENT> EVENT { get; set; }
        public DbSet<SEAT> SEAT { get; set; }
        public DbSet<SECTOR> SECTOR { get; set; }
        public DbSet<RESERVATION> RESERVATION { get; set; }
        public DbSet<AUDIT_LOG> AUDIT_LOG { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EVENT>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<USER>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<SECTOR>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();

                entity.Property(s => s.Price).HasPrecision(18, 2);

                entity.HasOne<EVENT>(s => s.EVENT)
                .WithMany(d => d.SECTORS)
                .HasForeignKey(m => m.EventId);

            });
            modelBuilder.Entity<SEAT>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(s => s.Version).IsConcurrencyToken();


                entity.HasOne<SECTOR>(s => s.SECTOR)
                .WithMany(d => d.SEATS)
                .HasForeignKey(m => m.SectorId);

            });
            modelBuilder.Entity<RESERVATION>(entity =>
            {
                entity.HasKey(E => E.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();

                entity.HasOne<USER>(s => s.USER)
                .WithMany(d => d.RESERVATIONS)
                .HasForeignKey(l => l.UserId);

                entity.HasOne<SEAT>(s => s.SEAT)
                .WithOne(d => d.RESERVATION)
                .HasForeignKey<RESERVATION>(x => x.SeatId);

            });
            modelBuilder.Entity<AUDIT_LOG>(entity =>
            {
                entity.HasKey(E => E.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                entity.HasOne<USER>(s => s.USER)
                .WithMany(d => d.AUDIT_LOGS)
                .HasForeignKey(l => l.UserId);
            });


            modelBuilder.Entity<EVENT>().HasData(
                new EVENT
                {
                    Id = 133,
                    Name = "Evento de Daddy Yankee",
                    EventDate = new DateTime(2026, 4, 15),
                    Venue = "Movistar Arena",
                    Status = "Available",
                });

            modelBuilder.Entity<SECTOR>().HasData(
                new SECTOR
                {
                    Id = 1,
                    EventId = 133,
                    Name = "Sector 1",
                    Price = 30000,
                    Capacity = 50
                },
                new SECTOR
                {
                    Id = 2,
                    EventId = 133,
                    Name = "Sector 2",
                    Price = 80300,
                    Capacity = 50

                });

            var seats = new List<SEAT>();
            int id = 1;

            // Sector 1 (50 butacas)
            for (int i = 1; i <= 50; i++)
            {
                seats.Add(new SEAT
                {
                    Id = Guid.Parse($"11111111-1111-1111-1111-{i.ToString("D12")}"),
                    SectorId = 1,
                    RowIdentifier = "Sector 1",
                    SeatNumber = i,
                    Status = SeatStatus.Available.ToString(),
                    Version = 1
                });
            }

            // Sector 2 (50 butacas)
            for (int i = 1; i <= 50; i++)
            {
                seats.Add(new SEAT
                {
                    Id = Guid.Parse($"22222222-2222-2222-2222-{i.ToString("D12")}"),
                    SectorId = 2,
                    RowIdentifier = "Sector 2",
                    SeatNumber = i,
                    Status = SeatStatus.Available.ToString(),
                    Version = 1
                });
            }

            modelBuilder.Entity<SEAT>().HasData(seats);

        }
    }
}
