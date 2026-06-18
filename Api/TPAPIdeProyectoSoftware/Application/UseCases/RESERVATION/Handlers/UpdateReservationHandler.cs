using Application.Interfaces.Handlers;
using Application.Interfaces.Repositories;

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
                return "Comando inválido";

            if (command.Id == null)
                return "Id inválido";

            if (command.UserId <= 0)
                return "Id de usuario inválido";

            if (string.IsNullOrWhiteSpace(command.Status))
                return "El estado es obligatorio";


            var existingReservation = await _reservationRepository.GetByIdAsync(command.Id);

            if (existingReservation == null)
                return "Reserva no encontrada";

            existingReservation.UserId = command.UserId;
            existingReservation.SeatId = command.SeatId;
            existingReservation.Status = command.Status;

            await _reservationRepository.UpdateAsync(existingReservation);

            return "Reserva actualizada correctamente";
        }
    }
}