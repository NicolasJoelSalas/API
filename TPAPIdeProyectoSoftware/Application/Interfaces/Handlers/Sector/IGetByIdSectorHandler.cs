using Application.DTOs;
using Application.DTOs.User;
using Application.UseCases;

namespace Application.Interfaces.Handlers
{
    public interface IGetByIdSectorHandler
    {
        Task<(SectorResponseDto? sector, string message)> Handle(GetByIdSectorQuery query);

    }
}
