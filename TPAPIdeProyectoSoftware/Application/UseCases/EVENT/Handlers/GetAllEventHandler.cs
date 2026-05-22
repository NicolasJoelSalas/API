using Application.DTOs.Event;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Queries;


namespace Application.UseCases.EVENT.Handlers
{
    public class GetAllEventHandler : IGetAllEventHandler
    {
        private readonly IEventRepository _eventRepository;

        public GetAllEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<(List<EventResponseDto> Events, string message)> Handle(GetAllEventQuery query)
        {
            var events = await _eventRepository.GetAllAsync();

            if (events == null || !events.Any())
                return (new List<EventResponseDto>(), "No hay eventos");

            var eventDtos = events.Select(eventEntity => new EventResponseDto
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                EventDate = eventEntity.EventDate,
                Venue = eventEntity.Venue,
                Status = eventEntity.Status
            }).ToList();

            return (eventDtos, "OK");
        }


    }
}