using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Domain.Entities;
using Domain.Enums;
using System.Linq.Expressions;

public class CreateReservationHandler : ICreateReservationHandler
{
    private readonly ICreateReservationCommand _command;
    private readonly IGetByIdUserQuery _queryUser;
    private readonly IGetByIdSeatQuery _querySeat;
    private readonly IGetAllReservationQuery _getAllReservationQuery;

    public CreateReservationHandler(ICreateReservationCommand command, IGetByIdUserQuery queryUser, IGetByIdSeatQuery querySeat, IGetAllReservationQuery getAllReservationQuery)
    {
        _command = command;
        _queryUser = queryUser;
        _querySeat = querySeat;
        _getAllReservationQuery = getAllReservationQuery;
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

        var seat = await _querySeat.GetById(dto.SeatId);

        if (seat == null)
            return "El asiento no existe";

        var listaDeReservaciones = await _getAllReservationQuery.GetAll();
        foreach (var elemento in listaDeReservaciones)
        {
            if (elemento.SeatId == dto.SeatId)
                return "El asiento ya se encuentra reservado";
        }

        var Reservation = new RESERVATION
        {
            UserId = dto.UserId,
            SeatId = dto.SeatId,
            Status = ReservationStatus.Pending, 
            ReservedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
        };

        await _command.ExecuteCreateReservation(Reservation);

        return "OK";
    }
}