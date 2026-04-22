using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application
{
    public class UpdateReservationCommand : IUpdateReservationCommand
    {
        private readonly IReservationRepository _repository;

        public UpdateReservationCommand(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteUpdateReservation(RESERVATION reser)
        {
            await _repository.UpdateAsync(reser);
        }
    }
}