using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Domain.Entities;
using Domain.Enums;

public class CreateReservationHandler : ICreateReservationHandler
{
    private readonly ICreateReservationCommand _command;
    private readonly IGetByIdUserQuery _queryUser;
    private readonly IGetByIdSeatQuery _querySeat;

    public CreateReservationHandler(ICreateReservationCommand command, IGetByIdUserQuery queryUser, IGetByIdSeatQuery _querySeat)
    {
        _command = command;
        _queryUser = queryUser;
        _querySeat = _querySeat;
    }

    public async Task<string> Handle(ReservationRequestDto dto)
    {
        if (dto == null)
            return "Datos inválidos";
          
        if (dto.UserId <= 0)
            return "El Id del usuario es obligatorio";

        var user = await _queryUser.GetById(dto.UserId);

        if (user == null)
            return "Usuario no existe";

        var seat = await _querySeat.GetById(dto.SeatId);

        if (seat == null)
            return "Seat no existe";

        if (dto.ExpiresAt <= DateTime.UtcNow)
            return "La fecha de expiración debe ser futura";

        var Reservation = new RESERVATION
        {
            UserId = dto.UserId,
            SeatId = dto.SeatId,
            Status = ReservationStatus.Pending, 
            ReservedAt = DateTime.UtcNow,
            ExpiresAt = dto.ExpiresAt
        };

        await _command.ExecuteCreateReservation(Reservation);

        return "OK";
    }
}