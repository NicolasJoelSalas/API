using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetAllSeatHandler : IGetAllSeatHandler
    {
        private readonly IGetAllSeatQuery _query;

        public GetAllSeatHandler(IGetAllSeatQuery query)
        {
            _query = query;
        }
        public async Task<(List<SeatResponseDto> Seat, string message)> Handle()
        {
            var Seat = await _query.GetAll();

            if (Seat == null || Seat.Count == 0)
                return (new List<SeatResponseDto>(), "No hay Seat registrados");

            return (Seat, "OK");
        }
    }
}
