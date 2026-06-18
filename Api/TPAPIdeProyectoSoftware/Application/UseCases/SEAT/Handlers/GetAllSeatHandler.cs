using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Queries;

namespace Application.UseCases
{
    public class GetAllSeatHandler : IGetAllSeatHandler
    {
        private readonly ISeatRepository _seatRepository;

        public GetAllSeatHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<(List<SeatResponseDto> seats, string message)> Handle(GetAllSeatQuery query)
        {
            var seats = await _seatRepository.GetAllAsync();

            if (seats == null || seats.Count == 0)
                return (new List<SeatResponseDto>(), "No hay asientos registrados");

            var response = seats.Select(seat => new SeatResponseDto
            {
                Id = seat.Id,
                SectorId = seat.SectorId,
                RowIdentifier = seat.RowIdentifier,
                SeatNumber = seat.SeatNumber,
                Status = seat.Status,
                Version = seat.Version
            }).ToList();

            return (response, "OK");
        }
    }
}
