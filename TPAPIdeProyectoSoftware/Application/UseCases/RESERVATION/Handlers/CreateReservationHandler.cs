using Application.DTOs.User;
using Application.Interfaces;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Domain.Entities;
using Domain.Enums;

public class CreateReservationHandler : ICreateReservationHandler
{
    private readonly ICreateReservationCommand _command;
    private readonly IUpdateSeatCommand _commandseat;
    private readonly IGetByIdUserQuery _queryUser;
    private readonly IGetEntitySeatQuery _EntitySeatQuery;
    private readonly ICreateAudit_LogCommand _auditLogCommand;
    public CreateReservationHandler(
        ICreateReservationCommand command,
        IGetByIdUserQuery queryUser,
        IUpdateSeatCommand commandseat,
        IGetEntitySeatQuery EntitySeatQuery,
        ICreateAudit_LogCommand auditLogCommand)
    {
        _command = command;
        _queryUser = queryUser;
        _commandseat = commandseat;
        _EntitySeatQuery = EntitySeatQuery;
        _auditLogCommand = auditLogCommand;
    }

    public async Task<string> Handle(ReservationRequestDto dto)
    {
        var auditLog_IntentoDeReserva = new AUDIT_LOG
        {
            UserId = dto.UserId,
            Action = "Intento de reserva",
            EntityType = "Seat",
            EntityId = dto.SeatId.ToString(),
            Details = $"UsuarioId: {dto.UserId}, AsientoId: {dto.SeatId}",
            CreatedAt = DateTime.UtcNow,
        };

        await _auditLogCommand.ExecuteCreateAudit_Log(auditLog_IntentoDeReserva);

        if (dto == null)
            return "Datos inválidos";

        if (dto.UserId <= 0)
            return "El Id del usuario es obligatorio";

        var user = await _queryUser.GetById(dto.UserId);
        if (user == null)
            return "El Usuario no existe";

        var seat = await _EntitySeatQuery.GetById(dto.SeatId);
        if (seat == null)
            return "El asiento no existe";

        // Validación rápida (opcional)
        if (seat.Status == SeatStatus.Reserved)
            return "El asiento ya está reservado";
        if (seat.Status == SeatStatus.Sold)
            return "El asiento ya está vendido";


        //Crear la reserva SOLO si el update fue exitoso
        var reservation = new RESERVATION
        {
            UserId = dto.UserId,
            SeatId = dto.SeatId,
            Status = ReservationStatus.Pending,
            ReservedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
        };
        //Intentar reservar el asiento (con concurrencia)
        seat.Status = SeatStatus.Reserved;
        await _commandseat.ExecuteUpdateSeat(seat);

        await _command.ExecuteCreateReservation(reservation);

        var auditLog_ReservaExitosa = new AUDIT_LOG
        {
            UserId = dto.UserId,
            Action = "Reserva exitosa",
            EntityType = "Reservation",
            EntityId = dto.SeatId.ToString(),
            Details = $"UsuarioId: {dto.UserId}, AsientoId: {dto.SeatId}, ReservaId: {reservation.Id}",
            CreatedAt = DateTime.UtcNow,
        };

        await _auditLogCommand.ExecuteCreateAudit_Log(auditLog_ReservaExitosa);


        return "OK";
    }
}