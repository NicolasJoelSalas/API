
using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.User;
using Application.UseCases.USER.Queries;
using Domain.Entities;
using System.Net.NetworkInformation;

namespace Application.UseCases
{
    public class UpdateSeatHandler : IUpdateSeatHandler
    {
        private readonly IUpdateSeatCommand _command;
        private readonly IGetByIdSeatQuery _query;
        private readonly IGetByIdSectorQuery _querySector;

        public UpdateSeatHandler(
            IUpdateSeatCommand command,
            IGetByIdSeatQuery query, IGetByIdSectorQuery querySector)
        {
            _command = command;
            _query = query;
            _querySector = querySector;
        }

        public async Task<string> Handle(Guid id, SeatRequestDto dto)
        {
            var Seatdto = await _query.GetById(id);
            if (Seatdto == null)
                return "Asiento no encontrado";

            if (dto.SectorId <= 0)
                return "El Id del sector es obligatorio";

            var sector = await _querySector.GetById(dto.SectorId);

            if (sector == null)
                return "El Sector no existe";

            if (string.IsNullOrWhiteSpace(dto.RowIdentifier))
                return "El Identificador de la fila es obligatorio";

            if (dto.SeatNumber <= 0 || dto.SeatNumber == null)
                return "El numero de asiento es obligatorio";

            if (string.IsNullOrWhiteSpace(dto.Status))
                return "El status es obligatorio";

            if (dto.Version <= 0 || dto.Version == null)
                return "La version es obligatorio";

            var seat = new SEAT
            {
                Id = id,
                SectorId = dto.SectorId,
                RowIdentifier = dto.RowIdentifier,
                SeatNumber = dto.SeatNumber,
                Status = dto.Status,
                Version = dto.Version,
            };

            await _command.ExecuteUpdateSeat(seat);

            return "Asiento actualizado correctamente";
        }
    }
}