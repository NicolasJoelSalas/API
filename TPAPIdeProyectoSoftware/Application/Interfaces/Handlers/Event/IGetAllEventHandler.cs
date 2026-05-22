using Application.DTOs.Event;
using Application.UseCases.EVENT.Queries;

namespace Application.Interfaces.Handlers.Event
{
    public interface IGetAllEventHandler
    {
        Task<(List<EventResponseDto> Events, string message)> Handle(GetAllEventQuery query);
    }
}