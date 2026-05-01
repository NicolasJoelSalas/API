using Application.DTOs.User;
using Application.Interfaces;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Domain.Enums;


namespace Application.UseCases.RESERVATION.Handlers
{
    public class CreateMultipleReservationHandler : ICreateMultipleReservationHandler
    {
        private readonly IGetReservedSeatIdsQuery _getReservedSeatsQuery;
        private readonly ICreateMultipleReservationCommand _createCommand;

        private readonly IUpdateSeatCommand _commandseat;
        private readonly IGetByIdUserQuery _queryUser;
        private readonly IGetEntitySeatQuery _EntitySeatQuery;
        private readonly ICreateAudit_LogCommand _auditLogCommand;

        public CreateMultipleReservationHandler(
            IGetReservedSeatIdsQuery getReservedSeatsQuery,
            ICreateMultipleReservationCommand createCommand,
            IUpdateSeatCommand commandseat,
            IGetByIdUserQuery queryUser,
            IGetEntitySeatQuery EntitySeatQuery,
            ICreateAudit_LogCommand auditLogCommand)
        {
            _getReservedSeatsQuery = getReservedSeatsQuery;
            _createCommand = createCommand;
            _commandseat = commandseat;
            _queryUser = queryUser;
            _EntitySeatQuery = EntitySeatQuery;
            _auditLogCommand = auditLogCommand;
        }

        public async Task<List<Guid>> Handle(CreateMultipleReservationDto dto)
        {
            if (dto == null)
                throw new Exception("Datos inválidos");

            if (dto.UserId <= 0)
                throw new Exception("El Id del usuario es obligatorio");

            if (dto.SeatIds == null || !dto.SeatIds.Any())
                throw new Exception("No se enviaron butacas");

            // Validar usuario
            var user = await _queryUser.GetById(dto.UserId);
            if (user == null)
                throw new Exception("El Usuario no existe");

            // AUDIT LOG: intento global
            var auditLogIntento = new Domain.Entities.AUDIT_LOG
            {
                UserId = dto.UserId,
                Action = "Intento de reserva",
                EntityType = "Reservation",
                EntityId = string.Join(",", dto.SeatIds),
                Details = $"UsuarioId: {dto.UserId}, Seats: {string.Join(",", dto.SeatIds)}",
                CreatedAt = DateTime.UtcNow,
            };

            await _auditLogCommand.ExecuteCreateAudit_Log(auditLogIntento);

            // Filtrar butacas ya reservadas
            var reservedSeatIds = await _getReservedSeatsQuery.Execute(dto.SeatIds);

            var availableSeatIds = dto.SeatIds
                .Except(reservedSeatIds)
                .ToList();

            if (!availableSeatIds.Any())
                throw new Exception("Todas las butacas seleccionadas ya están reservadas");

            var reservations = new List<Domain.Entities.RESERVATION>();

            foreach (var seatId in availableSeatIds)
            {
                // Obtener asiento
                var seat = await _EntitySeatQuery.GetById(seatId);

                if (seat == null)
                    continue;

                if (seat.Status == SeatStatus.Reserved || seat.Status == SeatStatus.Sold)
                    continue;

                // Marcar asiento
                seat.Status = SeatStatus.Reserved;
                await _commandseat.ExecuteUpdateSeat(seat);

                // Crear reserva
                var reservation = new Domain.Entities.RESERVATION
                {
                    UserId = dto.UserId,
                    SeatId = seatId,
                    Status = ReservationStatus.Pending,
                    ReservedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                };

                reservations.Add(reservation);
            }

            await _createCommand.Execute(reservations);

            foreach (var reservation in reservations)
            {
                var auditLog = new Domain.Entities.AUDIT_LOG
                {
                    UserId = dto.UserId,
                    Action = "Reserva exitosa",
                    EntityType = "Reservation",
                    EntityId = reservation.SeatId.ToString(),
                    Details = $"UsuarioId: {dto.UserId}, AsientoId: {reservation.SeatId}",
                    CreatedAt = DateTime.UtcNow,
                };

                await _auditLogCommand.ExecuteCreateAudit_Log(auditLog);
            }

            return reservations.Select(r => r.Id).ToList();
        }
    }
}
