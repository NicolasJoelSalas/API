using Application.DTOs.Event;
using Application.DTOs.User;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Queries.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.UseCases.EVENT.Queries
{
    public class GetEventByIdHandler : IGetByIdEventHandler
    {
        private readonly IGetAllByIdEventQuery _getEventByIdQuery;

        public GetEventByIdHandler(IGetAllByIdEventQuery getEventByIdQuery)
        {
            _getEventByIdQuery = getEventByIdQuery;
        }

        public async Task<(EventResponseDto events, string message)> GetByIdEventHandle(int id)
        {
            var evento = await _getEventByIdQuery.GetById(id);

            if (evento == null)
                return (new EventResponseDto(), "No hay eventos registrados");

            return (evento, "OK");
        }
    }
}