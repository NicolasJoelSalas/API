using Application.DTOs.Event;
using Application.DTOs.User;
using Application.UseCases.EVENT.Queries;

namespace Application.Interfaces.Handlers.Event
{
    public interface IGetByIdEventHandler
    {
        Task<(EventResponseDto Event, string message)> Handle(GetByIdEventQuery query);
    }
}