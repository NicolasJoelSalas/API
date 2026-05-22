using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Queries;


namespace Application.UseCases
{
    public class GetByIdSectorHandler : IGetByIdSectorHandler
    {
        private readonly ISectorRepository _sectorRepository;

        public GetByIdSectorHandler(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<(SectorResponseDto? sector, string message)> Handle(GetByIdSectorQuery query)
        {
            if (query == null)
                return (new SectorResponseDto(), "Query inválida");

            if (query.Id <= 0)
                return (null, "Id inválido");

            var sectorEntity = await _sectorRepository.GetByIdAsync(query.Id);

            if (sectorEntity == null)
                return (null, "Sector no encontrado");

            return (new SectorResponseDto
            {
                Id = sectorEntity.Id,
                Name = sectorEntity.Name,
                Price = sectorEntity.Price,
                Capacity = sectorEntity.Capacity
            }, "OK");
        }
    }
}
