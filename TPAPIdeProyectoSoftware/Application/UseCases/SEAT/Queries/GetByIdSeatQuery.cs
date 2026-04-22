using Application.DTOs.User;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Queries
{
    public class GetByIdSeatQuery : IGetByIdSeatQuery
    {
        private readonly ISeatRepository _repository;

        public GetByIdSeatQuery(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task<SeatResponseDto> GetById(Guid id)
        {
            return await _repository.Query().Where(x => x.Id == id).Select(x => new SeatResponseDto
            {
                SectorId = x.SectorId,
                RowIdentifier = x.RowIdentifier,
                SeatNumber = x.SeatNumber,
                Status = x.Status,
                Version = x.Version
            }).FirstOrDefaultAsync();
        }
    }
}