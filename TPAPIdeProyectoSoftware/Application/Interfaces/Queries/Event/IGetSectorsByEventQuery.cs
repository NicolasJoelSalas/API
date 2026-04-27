using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries.Event
{
    public interface IGetSectorsByEventQuery
    {
        Task<List<SectorResponseDto>> GetByEventId(int eventId);
    }
}
