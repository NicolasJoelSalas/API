using Application.DTOs.User;

namespace Application.Interfaces.Handlers.Sector
{
    public interface IGetSeatsBySectorHandler
    {
        Task<(List<SeatResponseDto>, string)> Handle(int sectorId);
    }
}
