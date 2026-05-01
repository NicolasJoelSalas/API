using Application.Interfaces.Command.Seat;
using Application.Interfaces.Repositories;

namespace Application.UseCases.SEAT.Commands
{
    public class MarkSeatsAsSoldCommand : IMarkSeatsAsSoldCommand
    {
        private readonly ISeatRepository _seatRepository;

        public MarkSeatsAsSoldCommand(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task Execute(List<Guid> reservationIds)
        {
            await _seatRepository.MarkAsSoldByReservationIds(reservationIds);
        }
    }
}