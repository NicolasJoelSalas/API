using Application.DTOs;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.Event;

public class GetSectorsByEventHandler : IGetSectorsByEventHandler
{
    private readonly IGetSectorsByEventQuery _query;

    public GetSectorsByEventHandler(IGetSectorsByEventQuery query)
    {
        _query = query;
    }

    public async Task<(List<SectorResponseDto>, string)> Handle(int eventId)
    {
        var sectors = await _query.GetByEventId(eventId);

        if (!sectors.Any())
            return (new List<SectorResponseDto>(), "No hay sectores");

        return (sectors, "OK");
    }
}