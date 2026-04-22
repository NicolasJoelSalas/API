using Application.DTOs.User;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Queries
{
    public class GetByIdReservationQuery : IGetByIdReservationQuery
    {
        private readonly IReservationRepository _repository;

        public GetByIdReservationQuery(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReservationResponseDto> GetById(Guid id)
        {
            return await _repository.Query().Where(x => x.Id == id).Select(x => new ReservationResponseDto
            {
                UserId = x.UserId,
                SeatId = x.SeatId,
                Status = x.Status,
                ReservedAt = x.ReservedAt,
                ExpiresAt = x.ExpiresAt,
            }).FirstOrDefaultAsync();
        }
    }
}