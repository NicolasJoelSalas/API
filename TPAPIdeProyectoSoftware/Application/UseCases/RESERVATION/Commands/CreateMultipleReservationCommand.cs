using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CreateMultipleReservationCommand : ICreateMultipleReservationCommand
    {
        private readonly IReservationRepository _reservationRepository;

        public CreateMultipleReservationCommand(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Execute(List<Domain.Entities.RESERVATION> reservations)
        {
            if (reservations == null || !reservations.Any())
                throw new Exception("No hay reservas para guardar");

            await _reservationRepository.AddRangeAsync(reservations);
        }
    }
}
