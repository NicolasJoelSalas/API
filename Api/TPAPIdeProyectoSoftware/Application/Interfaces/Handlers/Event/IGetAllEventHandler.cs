using Application.DTOs.Event;
using Application.UseCases.EVENT.Queries;

namespace Application.Interfaces.Handlers.Event
{
    public interface IGetAllEventHandler
    {
        Task<PagedResponse<EventResponseDto>> Handle(GetAllEventQuery query);
    }
}