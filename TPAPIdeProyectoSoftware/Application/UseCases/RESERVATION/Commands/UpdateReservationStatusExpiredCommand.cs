using Application.Interfaces.Command.Reservation;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.RESERVATION.Commands
{
    public class UpdateReservationStatusExpiredCommand : IUpdateReservationStatusExpiredCommand
    {
        private readonly IReservationRepository _repository;

        public UpdateReservationStatusExpiredCommand(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteUpdateReservation(Domain.Entities.RESERVATION reser, string status)
        {
            await _repository.UpdateStatusExpiredAsync(reser, status);
        }
    }
}
