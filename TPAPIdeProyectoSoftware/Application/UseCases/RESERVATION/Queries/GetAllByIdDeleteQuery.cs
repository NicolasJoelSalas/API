using Application.DTOs.User;
using Application.Interfaces.Queries.Reservation;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.RESERVATION.Queries
{
    public class GetAllByIdDeleteQuery : IGetAllByIdDeleteQuery
    {
        private readonly IReservationRepository _repository;

        public GetAllByIdDeleteQuery(IReservationRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<ReservationResponseDto>> GetAll()
        {
            return await _repository.Query()
                .OrderBy(x => x.Id)
                .Select(x => new ReservationResponseDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    SeatId = x.SeatId,
                    Status = x.Status,
                    ReservedAt = x.ReservedAt,
                    ExpiresAt = x.ExpiresAt,
                }).ToListAsync();
        }
    }
}
