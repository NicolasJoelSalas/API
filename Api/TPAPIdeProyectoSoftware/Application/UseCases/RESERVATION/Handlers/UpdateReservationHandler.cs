using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;
using Domain.Exceptions;

namespace Application.UseCases
{
    public class UpdateReservationHandler : IUpdateReservationHandler
    {
        private readonly IReservationRepository _reservationRepository;

        public UpdateReservationHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<string> Handle(UpdateReservationCommand command)
        {
            if (command == null)
                throw new DataNotFoundException("Comando inválido");

            if (command.Id == null)
                throw new MissingDataException("Id inválido");

            if (command.UserId <= 0)
                throw new MissingDataException("Id de usuario inválido");

            if (string.IsNullOrWhiteSpace(command.Status))
                throw new MissingDataException("El estado es obligatorio");


            var existingReservation = await _reservationRepository.GetByIdAsync(command.Id);

            if (existingReservation == null)
                throw new DataNotFoundException("Reserva no encontrada");

            existingReservation.UserId = command.UserId;
            existingReservation.SeatId = command.SeatId;
            existingReservation.Status = command.Status;

            await _reservationRepository.UpdateAsync(existingReservation);

            return "Reserva actualizada correctamente";
        }
    }
}