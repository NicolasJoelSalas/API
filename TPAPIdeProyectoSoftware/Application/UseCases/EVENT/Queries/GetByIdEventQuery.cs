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
    public class GetByIdEventQuery : IGetByIdEventQuery
    {
        private readonly IEventRepository _eventRepository;

        public GetByIdEventQuery(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventResponseDto> GetById(int id)
        {
            return await _eventRepository.Query().Where(x => x.Id == id).Select(x => new EventResponseDto
            {
                Name = x.Name,
                EventDate = x.EventDate,
                Venue = x.Venue,
                Status = x.Status
            }).FirstOrDefaultAsync();
        }

    }
}