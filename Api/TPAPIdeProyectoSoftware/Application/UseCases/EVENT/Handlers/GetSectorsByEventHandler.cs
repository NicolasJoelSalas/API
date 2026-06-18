using Application.DTOs;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Queries;

public class GetSectorsByEventHandler : IGetSectorsByEventHandler
{
    private readonly ISectorRepository _sectorRepository;

    public GetSectorsByEventHandler( ISectorRepository sectorRepository)
    {

        _sectorRepository = sectorRepository;
    }

    public async Task<(List<SectorResponseDto>, string)> Handle(int eventId)
    {
        var sectors = await _sectorRepository.GetSectorsByEventId(eventId);

        if (!sectors.Any())
            return (new List<SectorResponseDto>(), "No hay sectores para este evento");

        var sectorDtos = sectors.Select(sector => new SectorResponseDto
        {
            Id = sector.Id,
            Name = sector.Name,
            Price = sector.Price
        }).ToList();

        return (sectorDtos, "OK");
    }
}
