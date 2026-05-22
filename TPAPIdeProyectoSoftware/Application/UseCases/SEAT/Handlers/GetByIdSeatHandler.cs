using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Queries;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetByIdSeatHandler : IGetByIdSeatHandler
    {
        private readonly ISeatRepository _seatRepository;

        public GetByIdSeatHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<(SeatResponseDto? seat, string message)> Handle(GetByIdSeatQuery query)
        {
            if (query == null)
                return (new SeatResponseDto(), "Query inválida");

            if (query.Id == null)
                return (null, "Id inválido");

            var seatEntity = await _seatRepository.GetByIdAsync(query.Id);

            if (seatEntity == null)
                return (null, "Asiento no encontrado");

            return (new SeatResponseDto
            {
                Id = seatEntity.Id,
                SectorId = seatEntity.SectorId,
                RowIdentifier = seatEntity.RowIdentifier,
                SeatNumber = seatEntity.SeatNumber,
                Status = seatEntity.Status,
                Version = seatEntity.Version
            }, "OK");
               
        }

    }
}
