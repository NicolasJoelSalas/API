using Application.DTOs;
using Application.DTOs.Event;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Repositories;



namespace Application.UseCases.EVENT.Queries
{
    public class GetEventByIdHandler : IGetByIdEventHandler
    {
        private readonly IEventRepository _eventRepository;

        public GetEventByIdHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<(EventResponseDto Event, string message)> Handle(GetByIdEventQuery query)
        {
            if (query == null)
                return (new EventResponseDto(), "Query inválida");

            if (query.Id < 0)
                return (new EventResponseDto(), "Id inválido");

            var eventEntity = await _eventRepository.GetByIdAsync(query.Id);

            if (eventEntity == null)
                return (new EventResponseDto(), "Evento no encontrado");

            return (new EventResponseDto
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                EventDate = eventEntity.EventDate,
                Venue = eventEntity.Venue,
                Status = eventEntity.Status
            }, "OK");       
        }


    }
}