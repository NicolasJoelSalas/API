using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;

namespace Application.UseCases
{
    public class DeleteSeatHandler : IDeleteSeatHandler
    {
        private readonly ISeatRepository _seatRepository;

        public DeleteSeatHandler(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<string> Handle(DeleteSeatCommand command)
        {
            if (command == null || command.Id == Guid.Empty)
                return "Id inválido";

            var seat = await _seatRepository.GetByIdAsync(command.Id);

            if (seat == null)
                return "Seat no encontrado";

            await _seatRepository.DeleteAsync(seat);

            return "Seat eliminado correctamente";
        }
    }
}