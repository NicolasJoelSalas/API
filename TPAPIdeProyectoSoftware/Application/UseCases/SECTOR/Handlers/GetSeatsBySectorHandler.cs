using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Handlers.Sector;
using Application.Interfaces.Repositories;


namespace Application.UseCases.SECTOR.Handlers
{
    public class GetSeatsBySectorHandler : IGetSeatsBySectorHandler 
    {
        private readonly ISeatRepository _seatRepository;

        public GetSeatsBySectorHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<(List<SeatResponseDto>, string)> Handle(int sectorId)
        {
            var seats = await _seatRepository.GetSeatBySectorId(sectorId);

            if (seats == null || !seats.Any())
                return (new List<SeatResponseDto>(), "No hay asientos");
            
            var seatDtos = seats.Select(seat => new SeatResponseDto
            {
                Id = seat.Id,
                SectorId = seat.SectorId,
                SeatNumber = seat.SeatNumber,
                Status = seat.Status,
                Version = seat.Version}).ToList();

            return (seatDtos, "OK");
        }
    }
}

