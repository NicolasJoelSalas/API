using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.Event;
using Application.Interfaces.Queries.User;
using Application.UseCases.USER.Queries;
using Domain.Entities;

namespace Application.UseCases
{
    public class UpdateSectorHandler : IUpdateSectorHandler
    {
        private readonly IUpdateSectorCommand _command;
        private readonly IGetByIdEventQuery _queryEvent;

        public UpdateSectorHandler(
            IUpdateSectorCommand command,
            IGetByIdEventQuery _queryEvent)
            
        {
            _command = command;
            _queryEvent = _queryEvent;
        }

        public async Task<string> Handle(int id, SectorRequestDto dto)
        {
            if (dto == null)
                return "Datos inválidos";

            var eventt = await _queryEvent.GetById(dto.EventId);

            if (eventt == null)
                return "Event no existe";

            if (string.IsNullOrWhiteSpace(dto.Name))
                return "El name es obligatorio";

            if (dto.Price < 0)
                return "El price es obligatorio";

            if (dto.Capacity < 0)
                return "La capacity es obligatoria";

            var sector = new SECTOR
            {
                EventId = dto.EventId,
                Name = dto.Name,
                Price = dto.Price,
                Capacity = dto.Capacity
            };

            await _command.ExecuteUpdateSector(sector);

            return "OK";

        }
    }
}