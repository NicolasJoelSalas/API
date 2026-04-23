using Application.DTOs.Event;
using Application.DTOs.User;
using Application.Interfaces.Queries.Event;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.EVENT.Queries
{
    public class GetAllEventQuery : IGetAllEventQuery
    {
        private readonly IEventRepository _eventRepository;

        public GetAllEventQuery(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<EventResponseDto>> GetAllEvent()
        {
            return await _eventRepository.Query()
                .OrderBy(x => x.Name)
                .Select(x => new EventResponseDto
                {
                    Name = x.Name,
                    EventDate = x.EventDate,
                    Venue = x.Venue,
                    Status = x.Status
                }).ToListAsync();
        }
    }
}