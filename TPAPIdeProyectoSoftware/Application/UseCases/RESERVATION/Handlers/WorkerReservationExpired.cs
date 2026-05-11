    using Application.Interfaces.Command;
    using Application.Interfaces.Command.Reservation;
    using Application.Interfaces.Command.Seat;
using Application.Interfaces.Queries.Reservation;
using Application.Interfaces.Queries.User;
    using Application.UseCases.USER.Queries;
    using Domain.Entities;
    using Domain.Enums;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var reservationQuery =
                            scope.ServiceProvider.GetRequiredService<IGetAllByIdDeleteQuery>();

                        var reservationCommand =
                            scope.ServiceProvider.GetRequiredService<IUpdateReservationStatusExpiredCommand>();

                        var seatCommand =
                            scope.ServiceProvider.GetRequiredService<IMarkSeatsAsAvailableCommand>();

                        var auditLogCommand =
                            scope.ServiceProvider.GetRequiredService<ICreateAudit_LogCommand>();

                        var eliminarReservaCommand =
                            scope.ServiceProvider.GetRequiredService<IDeleteReservationCommand>();

                        var getByIdReservationQuery =
                            scope.ServiceProvider.GetRequiredService<IGetByIdReservationQuery>();

                        var reservations = await reservationQuery.GetAll();

                        foreach (var elemento in reservations)
                        {
                            if (elemento.ExpiresAt < DateTime.UtcNow &&
                                elemento.Status == ReservationStatus.Pending)
                            {
                                Domain.Entities.RESERVATION ReservaDePrueba1 = new Domain.Entities.RESERVATION
                                {
                                    Id = elemento.Id,
                                    UserId = elemento.UserId,
                                    SeatId = elemento.SeatId,
                                    Status = elemento.Status,
                                    ExpiresAt = elemento.ExpiresAt
                                };
                                await reservationCommand.ExecuteUpdateReservation(
                                    ReservaDePrueba1,
                                    ReservationStatus.Expired.ToString()
                                    );
                                // Nose para que voy a cambiar el estado si despues lo voy a eliminar a la reserva porque la 
                                //DbAppContext solo permite crear una reserva por asiento, independientemente del estado de la reserva
                                await seatCommand.Execute(elemento.SeatId);
                                var auditLog = new Domain.Entities.AUDIT_LOG
                                {
                                    Action = "Reserva Expirada",
                                    UserId = elemento.UserId,
                                    EntityType = "Reservation",
                                    EntityId = elemento.Id.ToString(),
                                    Details = $"Reserva expirada para {elemento.Id}",
                                    CreatedAt = DateTime.UtcNow,
                                };

                                await auditLogCommand.ExecuteCreateAudit_Log(auditLog);




                                var auditLog2 = new Domain.Entities.AUDIT_LOG
                                {
                                    Action = "Reserva expirada eliminada",
                                    UserId = elemento.UserId,
                                    EntityType = "Reservation",
                                    EntityId = elemento.Id.ToString(),
                                    Details = $"Reserva expirada eliminada para {elemento.Id}",
                                    CreatedAt = DateTime.UtcNow,
                                };

                                await auditLogCommand.ExecuteCreateAudit_Log(auditLog2);
                                
                                await eliminarReservaCommand.ExecuteDeleteReservation(elemento.Id);


                            }
                        }
                    }

                    // Espera 1 minuto antes de volver a revisar
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
            }
            catch(OperationCanceledException)
            {
                Console.WriteLine("detener el proceso de expiración de reservas");
            }   
        }
    }
}
