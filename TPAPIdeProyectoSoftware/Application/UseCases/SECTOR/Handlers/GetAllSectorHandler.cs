using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Queries;
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
        private readonly ISectorRepository _sectorRepository;

        public GetAllSectorHandler(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<(List<SectorResponseDto> sectors, string message)> Handle(GetAllSectorQuery query)
        {
            var sectors = await _sectorRepository.GetAllAsync();

            if (sectors == null || sectors.Count == 0)
                return (new List<SectorResponseDto>(), "No hay sectores registrados");

            var response = sectors.Select(sector => new SectorResponseDto
            {
                Id = sector.Id,
                Name = sector.Name,
                Price = sector.Price,
                Capacity = sector.Capacity
            }).ToList();

            return (response, "OK");
        }
    }
}
