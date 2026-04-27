using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries.Sector
{
    public interface IGetSeatsBySectorQuery
    {
        Task<List<SeatResponseDto>> GetBySectorId(int sectorId);
    }
}
