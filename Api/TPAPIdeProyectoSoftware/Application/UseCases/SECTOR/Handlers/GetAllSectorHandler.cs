using Application.DTOs;
using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;

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
