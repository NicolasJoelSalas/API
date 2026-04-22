using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Commands
{
    public class CreateSeatCommand : ICreateSeatCommand
    {
        private readonly ISeatRepository _repository;

        public CreateSeatCommand(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteCreateSeat(Domain.Entities.SEAT seat)
        {
            await _repository.AddAsync(seat);
        }

    }
}