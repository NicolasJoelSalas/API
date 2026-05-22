using Application.DTOs;

namespace Application.Interfaces.Handlers.Event
{
    public interface IGetSectorsByEventHandler 
    {
        Task<(List<SectorResponseDto>, string)> Handle(int eventId);
    }
}
