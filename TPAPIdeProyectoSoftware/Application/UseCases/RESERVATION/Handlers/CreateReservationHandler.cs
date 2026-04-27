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

    public CreateReservationHandler(
        ICreateReservationCommand command,
        IGetByIdUserQuery queryUser,
        IUpdateSeatCommand commandseat,
        IGetEntitySeatQuery EntitySeatQuery)
    {
        _command = command;
        _queryUser = queryUser;
        _commandseat = commandseat;
        _EntitySeatQuery = EntitySeatQuery;
    }

    public async Task<string> Handle(ReservationRequestDto dto)
    {
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

        return "OK";
    }
}