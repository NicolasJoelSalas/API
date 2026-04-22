using Application.DTOs.User;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Queries
{
    public class GetAllSeatQuery : IGetAllSeatQuery
    {
        private readonly ISeatRepository _repository;

        public GetAllSeatQuery(ISeatRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<SeatResponseDto>> GetAll()
        {
            return await _repository.Query()
                .OrderBy(x => x.Id)
                .Select(x => new SeatResponseDto
                {
                    SectorId = x.SectorId,
                    RowIdentifier = x.RowIdentifier,
                    SeatNumber = x.SeatNumber,
                    Status = x.Status,
                    Version = x.Version
                }).ToListAsync();
        }
    }
}
