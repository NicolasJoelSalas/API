using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.EVENT.Commands;
using Application.UseCases.USER.Commands;
using Domain.Exceptions;

namespace Application.UseCases
{
    public class DeleteReservationHandler : IDeleteReservationHandler
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<string> Handle(DeleteReservationCommand command)
        {
            if (command == null)
                throw new DataNotFoundException("Comando inválido");

            if (command.Id == null)
                throw new MissingDataException("Id inválido");

            var reservationEntity = await _reservationRepository.GetByIdAsync(command.Id);

            if (reservationEntity == null)
                throw new DataNotFoundException("Reserva no encontrada");

            await _reservationRepository.DeleteAsync(reservationEntity.Id);

            return "Reserva eliminada correctamente";
        }
    }
}