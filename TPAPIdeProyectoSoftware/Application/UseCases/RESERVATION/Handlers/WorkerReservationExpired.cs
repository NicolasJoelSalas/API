using Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.UseCases.RESERVATION.Handlers
{
    public class WorkerReservationExpired : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public WorkerReservationExpired(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var reservationRepository =
                        scope.ServiceProvider.GetRequiredService<IReservationRepository>();

                    var seatRepository =
                        scope.ServiceProvider.GetRequiredService<ISeatRepository>();

                    var auditLogRepository =
                        scope.ServiceProvider.GetRequiredService<IAudit_LogRepository>();

                    // Trae reservas pendientes
                    var reservations = await reservationRepository.GetPendingReservationsAsync();

                    foreach (var reservation in reservations)
                    {
                        if (reservation.ExpiresAt <= DateTime.UtcNow)
                        {
                            // Liberar asiento
                            var seat = await seatRepository.GetByIdAsync(reservation.SeatId);

                            if (seat != null)
                            {
                                seat.Status = "Available";
                            }

                            // Crear log
                            var auditLog = new Domain.Entities.AUDIT_LOG
                            {
                                Action = "Reserva expirada eliminada",
                                UserId = reservation.UserId,
                                EntityType = "Reservation",
                                EntityId = reservation.Id.ToString(),
                                Details = $"Reserva expirada eliminada para {reservation.Id}",
                                CreatedAt = DateTime.UtcNow
                            };

                            await auditLogRepository.AddAsync(auditLog);

                            // Eliminar reserva
                            await reservationRepository.DeleteAsync(reservation.Id);
                        }
                    }

                    // Guardar todo junto
                    await reservationRepository.SaveChangesAsync();

                    // Esperar 1 minuto
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Proceso de expiración detenido");
            }
        }
    }
}