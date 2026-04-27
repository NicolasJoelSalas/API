using Application.DTOs.User;
using Application.Interfaces.Handlers.Sector;
using Application.Interfaces.Queries.Sector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.SECTOR.Handlers
{
    public class GetSeatsBySectorHandler : IGetSeatsBySectorHandler
    {
        private readonly IGetSeatsBySectorQuery _query;

        public GetSeatsBySectorHandler(IGetSeatsBySectorQuery query)
        {
            _query = query;
        }

        public async Task<(List<SeatResponseDto>, string)> Handle(int sectorId)
        {
            var seats = await _query.GetBySectorId(sectorId);

            if (seats == null || !seats.Any())
                return (new List<SeatResponseDto>(), "No hay asientos");

            return (seats, "OK");
        }
    }
}

