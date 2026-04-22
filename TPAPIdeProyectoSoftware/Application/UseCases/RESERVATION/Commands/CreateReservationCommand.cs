using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Commands
{
    public class CreateReservationCommand : ICreateReservationCommand
    {
        private readonly IReservationRepository _repository;

        public CreateReservationCommand(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteCreateReservation(Domain.Entities.RESERVATION reser)
        {
            await _repository.AddAsync(reser);
        }

    }
}