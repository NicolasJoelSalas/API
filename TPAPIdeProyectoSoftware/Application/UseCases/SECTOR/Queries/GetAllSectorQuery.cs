using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetAllSectorQuery : IGetAllSectorQuery
    {
        private readonly ISectorRepository _repository;

        public GetAllSectorQuery(ISectorRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<SectorResponseDto>> GetAll()
        {
            return await _repository.Query()
                .OrderBy(x => x.Id)
                .Select(x => new SectorResponseDto
                {
                    EventId = x.EventId,
                    Name = x.Name,
                    Price = x.Price,
                    Capacity = x.Capacity,
                }).ToListAsync();
        }
    }
}
