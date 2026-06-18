
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;
using Domain.Entities;
using Domain.Enums;

public class CreateSeatHandler : ICreateSeatHandler
{
    private readonly ISectorRepository _sectorRepository;
    private readonly ISeatRepository _seatRepository;

    public CreateSeatHandler(
        ISectorRepository sectorRepository,
        ISeatRepository seatRepository)
    {
        _sectorRepository = sectorRepository;
        _seatRepository = seatRepository;
    }

    public async Task<string> Handle(CreateSeatCommand command)
    {
        if (command == null)
            return "Datos inválidos";

        if (command.SectorId <= 0)
            return "El Id del sector es obligatorio";

        var sector = await _sectorRepository.GetByIdAsync(command.SectorId);

        if (sector == null)
            return "El Sector indicado no existe";

        if (string.IsNullOrWhiteSpace(command.RowIdentifier))
            return "El identificador de fila es obligatorio";

        if (command.SeatNumber <= 0)
            return "El número de asiento es obligatorio";

        if (command.Version <= 0)
            return "La version es obligatoria";

        var seat = new SEAT
        {
            SectorId = command.SectorId,
            RowIdentifier = command.RowIdentifier,
            SeatNumber = command.SeatNumber,
            Status = SeatStatus.Available.ToString(),
            Version = command.Version
        };

        await _seatRepository.AddAsync(seat);

        return "OK";
    }
}