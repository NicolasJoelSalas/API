using Application.DTOs;
using Application.DTOs.User;
using Application.UseCases;


namespace Application.Interfaces.Handlers
{
    public interface IGetAllSectorHandler
    {
        Task<(List<SectorResponseDto> sectors, string message)> Handle(GetAllSectorQuery query);
    }
}
