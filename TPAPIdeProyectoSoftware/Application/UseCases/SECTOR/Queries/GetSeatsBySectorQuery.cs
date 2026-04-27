using Application.DTOs.User;
using Application.Interfaces.Queries.Sector;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.SECTOR.Queries
{
    public class GetSeatsBySectorQuery : IGetSeatsBySectorQuery
    {
        private readonly ISeatRepository _repositorySeat;

        public GetSeatsBySectorQuery(ISeatRepository repositorySeat)
        {
            _repositorySeat = repositorySeat;
        }

        public async Task<List<SeatResponseDto>> GetBySectorId(int sectorId)
        {
            return await _repositorySeat.Query()
                .Where(x => x.SectorId == sectorId)
                .Select(x => new SeatResponseDto
                {
                    Id = x.Id,
                    RowIdentifier = x.RowIdentifier,
                    SeatNumber = x.SeatNumber,
                    Status = x.Status
                })
                .ToListAsync();
        }
        
    }
}
