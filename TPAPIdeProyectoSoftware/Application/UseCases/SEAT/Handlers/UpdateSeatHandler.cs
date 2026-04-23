using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.User;
using Application.UseCases.USER.Queries;
using Domain.Entities;

namespace Application.UseCases
{
    public class UpdateSeatHandler : IUpdateSeatHandler
    {
        private readonly IUpdateSeatCommand _command;
        private readonly IGetByIdSeatQuery _query;
        private readonly IGetByIdSectorQuery _querySector;

        public UpdateSeatHandler(
            IUpdateSeatCommand command,
            IGetByIdSeatQuery query, IGetByIdSectorQuery _querySector)
        {
            _command = command;
            _query = query;
            _querySector = _querySector;
        }

        public async Task<string> Handle(Guid id, SeatRequestDto dto)
        {
            if (dto == null)
                return "Datos inválidos";

            if (dto.SectorId <= 0)
                return "El Id del sector es obligatorio";

            var sector = await _querySector.GetById(dto.SectorId);

            if (sector == null)
                return "Sector no existe";

            if (string.IsNullOrWhiteSpace(dto.RowIdentifier))
                return "El RowIdentifier es obligatorio";

            if (dto.SeatNumber <= 0 || dto.SeatNumber == null)
                return "La version es obligatorio";

            if (string.IsNullOrWhiteSpace(dto.Status))
                return "El status es obligatorio";

            if (dto.Version <= 0 || dto.Version == null)
                return "La version es obligatorio";

            var seat = new SEAT
            {
                SectorId = dto.SectorId,
                RowIdentifier = dto.RowIdentifier,
                SeatNumber = dto.SeatNumber,
                Status = dto.Status,
                Version = dto.Version,
            };

            await _command.ExecuteUpdateSeat(seat);

            return "Audit_Log actualizado correctamente";
        }
    }
}