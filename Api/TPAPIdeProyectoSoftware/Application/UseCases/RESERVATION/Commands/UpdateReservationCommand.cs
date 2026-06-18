using Application.Interfaces.Repositories;

namespace Application
{
    public class UpdateReservationCommand 
    {
        public Guid Id { get; }
        public int UserId { get; }
        public Guid SeatId { get; }
        public string Status { get; }
        public DateTime ReservedAt { get; }
        public DateTime ExpiresAt { get; }

        public UpdateReservationCommand(
            Guid id,
            int userId,
            Guid seatId,
            string status,
            DateTime reservedAt,
            DateTime expiresAt)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            Status = status;
            ReservedAt = reservedAt;
            ExpiresAt = expiresAt;
        }

    }
}