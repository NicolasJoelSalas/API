using MediatR;

namespace Application.UseCases.RESERVATION.Commands
{
    public class CreateReservationCommand
    {
        public int UserId { get; }
        public List<Guid> SeatIds { get; }

        public CreateReservationCommand(int userId, List<Guid> seatIds)
        {
            UserId = userId;
            SeatIds = seatIds;
        }
    }
}