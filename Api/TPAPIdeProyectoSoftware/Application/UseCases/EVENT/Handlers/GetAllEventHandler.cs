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

        public async Task<PagedResponse<EventResponseDto>> Handle(GetAllEventQuery query)
        {
            var (events, total) = await _eventRepository.GetPagedAsync(query.Page, query.PageSize);

            var eventDtos = events.Select(e => new EventResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                EventDate = e.EventDate,
                Venue = e.Venue,
                Status = e.Status
            }).ToList();

            return new PagedResponse<EventResponseDto>
            {
                Data = eventDtos,
                Total = total,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }


    }
}