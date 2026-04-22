using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application
{
    public class UpdateSeatCommand : IUpdateSeatCommand
    {
        private readonly ISeatRepository _repository;

        public UpdateSeatCommand(ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteUpdateSeat(SEAT seat)
        {
            await _repository.UpdateAsync(seat);
        }
    }
}