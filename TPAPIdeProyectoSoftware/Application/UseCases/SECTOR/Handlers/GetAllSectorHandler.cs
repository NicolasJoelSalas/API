using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetAllSectorHandler : IGetAllSectorHandler
    {
        private readonly IGetAllSectorQuery _query;

        public GetAllSectorHandler(IGetAllSectorQuery query)
        {
            _query = query;
        }
        public async Task<(List<SectorResponseDto> Sector, string message)> Handle()
        {
            var Sector = await _query.GetAll();

            if (Sector == null || Sector.Count == 0)
                return (new List<SectorResponseDto>(), "No hay un sector sin el id indicado");

            return (Sector, "OK");
        }
    }
}
