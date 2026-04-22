using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetByIdSeatHandler : IGetByIdSeatHandler
    {
        private readonly IGetByIdSeatQuery _query;

        public GetByIdSeatHandler(IGetByIdSeatQuery query)
        {
            _query = query;
        }
        public async Task<(SeatResponseDto Seat, string message)> Handle(Guid id)
        {
            var seat = await _query.GetById(id);

            if (seat == null)
                return (new SeatResponseDto(), "No hay Reservation registrados");

            return (seat, "OK");
        }

    }
}
