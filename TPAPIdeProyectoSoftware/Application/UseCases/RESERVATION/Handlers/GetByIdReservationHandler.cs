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
    public class GetByIdReservationHandler : IGetByIdReservationHandler
    {
        private readonly IGetByIdReservationQuery _query;

        public GetByIdReservationHandler(IGetByIdReservationQuery query)
        {
            _query = query;
        }
        public async Task<(ReservationResponseDto Reservation, string message)> Handle(Guid id)
        {
            var reservation = await _query.GetById(id);

            if (reservation == null)
                return (new ReservationResponseDto(), "No hay Reservaciones registrados");

            return (reservation, "OK");
        }

    }
}
