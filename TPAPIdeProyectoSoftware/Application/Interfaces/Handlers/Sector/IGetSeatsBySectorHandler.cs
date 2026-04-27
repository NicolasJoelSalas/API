using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Sector
{
    public interface IGetSeatsBySectorHandler
    {
        Task<(List<SeatResponseDto>, string)> Handle(int sectorId);
    }
}
