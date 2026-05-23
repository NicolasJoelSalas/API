using MediatR;

namespace Application.UseCases.RESERVATION.Commands
{
    public class CreateReservationCommand
    {
        public int UserId { get; }
        public Guid SeatId { get; }

        public CreateReservationCommand(int userId, Guid seatId)
        {
            UserId = userId;
            SeatId = seatId;
        }
    }
}