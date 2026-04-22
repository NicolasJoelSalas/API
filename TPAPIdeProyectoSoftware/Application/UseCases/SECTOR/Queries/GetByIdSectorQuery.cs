using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetByIdSectorQuery : IGetByIdSectorQuery
    {
        private readonly ISectorRepository _repository;

        public GetByIdSectorQuery(ISectorRepository repository)
        {
            _repository = repository;
        }

        public async Task<SectorResponseDto> GetById(int id)
        {
            return await _repository.Query().Where(x => x.Id == id).Select(x => new SectorResponseDto
            {
                EventId = x.EventId,
                Name = x.Name,
                Price = x.Price,
                Capacity = x.Capacity,
            }).FirstOrDefaultAsync();
        }
    }
}