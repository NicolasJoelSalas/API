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
    public class GetByIdSectorHandler : IGetByIdSectorHandler
    {
        private readonly IGetByIdSectorQuery _query;

        public GetByIdSectorHandler(IGetByIdSectorQuery query)
        {
            _query = query;
        }
        public async Task<(SectorResponseDto Sector, string message)> Handle(int id)
        {
            var sector = await _query.GetById(id);

            if (sector == null)
                return (new SectorResponseDto(), "No hay Sector registrados");

            return (sector, "OK");
        }

    }
}
