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
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Xml.Linq;

namespace Application.UseCases
{
    public class UpdateSectorHandler : IUpdateSectorHandler
    {
        private readonly IUpdateSectorCommand _command;
        private readonly IGetByIdEventQuery _queryEvent;
        private readonly IGetByIdSectorQuery _getByIdSectorQuery;

        public UpdateSectorHandler(
            IUpdateSectorCommand command,
            IGetByIdEventQuery queryEvent, 
            IGetByIdSectorQuery getByIdSectorQuery)
        {
            _command = command;
            _queryEvent = queryEvent;
            _getByIdSectorQuery = getByIdSectorQuery;
        }

        public async Task<string> Handle(int id, SectorRequestDto dto)
        {
            if (dto == null)
                return "Datos inválidos";

            var sector = await _getByIdSectorQuery.GetById(id);

            if(sector == null)
                return "El Sector no existe";
            

            var eventt = await _queryEvent.GetById(dto.EventId);

            if (eventt == null)
                return "El Evento no existe";

            if (string.IsNullOrWhiteSpace(dto.Name))
                return "El nombre del sector es obligatorio";

            if (dto.Price.CompareTo(0) <= 0)
                return "Ingrese un precio mayor a 0";

            if (dto.Capacity <= 0)
                return "Ingrese una capacidad mayor a 0";


            var updatedsector = new SECTOR
            {
                Id = id,
                EventId = dto.EventId,
                Name = dto.Name,
                Price = dto.Price,
                Capacity = dto.Capacity
            };

            updatedsector.EventId = dto.EventId;
            updatedsector.Name = dto.Name;
            updatedsector.Price = dto.Price;
            updatedsector.Capacity = dto.Capacity;



            await _command.ExecuteUpdateSector(updatedsector);

            return "OK";

        }
    }
}