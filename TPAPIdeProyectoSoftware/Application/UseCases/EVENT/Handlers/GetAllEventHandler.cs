using Application.DTOs.Event;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Queries.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EVENT.Handlers
{
    public class GetAllEventHandler : IGetAllEventHandler
    {
        private readonly IGetAllEventQuery _getAllEventQuery;

        public GetAllEventHandler(IGetAllEventQuery getAllEventQuery)
        {
            _getAllEventQuery = getAllEventQuery;
        }

        public async Task<(List<EventResponseDto> events, string message)> GetAllEventHandle()
        {
            var events = await _getAllEventQuery.GetAllEvent();
            if (events == null || !events.Any())
            {
                return (new List<EventResponseDto>(), "No hay eventos registrados.");
            }
            return (events, "OK");
        }

    }
}