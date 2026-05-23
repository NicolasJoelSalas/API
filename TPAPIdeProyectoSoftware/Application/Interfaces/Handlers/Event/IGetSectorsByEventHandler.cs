using Application.DTOs;
using Application.UseCases.EVENT.Queries;

namespace Application.Interfaces.Handlers.Event
{
    public interface IGetSectorsByEventHandler 
    {
        Task<(List<SectorResponseDto>, string)> Handle(int id);
    }
}
