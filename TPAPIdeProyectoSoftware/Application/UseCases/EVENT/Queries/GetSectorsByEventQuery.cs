using Application.DTOs;
using Application.Interfaces.Queries.Event;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EVENT.Queries
{
    public class GetSectorsByEventQuery : IGetSectorsByEventQuery
    {
        private readonly ISectorRepository _repositorySector;

        public GetSectorsByEventQuery(ISectorRepository repositorySector)
        {
            _repositorySector = repositorySector;
        }
        public async Task<List<SectorResponseDto>> GetByEventId(int eventId)
        {
            return await _repositorySector.Query()
                .Where(x => x.EventId == eventId)
                .Select(x => new SectorResponseDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}
