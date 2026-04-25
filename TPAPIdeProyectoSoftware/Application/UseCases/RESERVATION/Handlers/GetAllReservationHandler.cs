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
    public class GetAllReservationHandler : IGetAllReservationHandler
    {
        private readonly IGetAllReservationQuery _query;

        public GetAllReservationHandler(IGetAllReservationQuery query)
        {
            _query = query;
        }
        public async Task<(List<ReservationResponseDto> Reservation, string message)> Handle()
        {
            var Reservation = await _query.GetAll();

            if (Reservation == null || Reservation.Count == 0)
                return (new List<ReservationResponseDto>(), "No hay reservaciones registrados");

            return (Reservation, "OK");
        }
    }
}
