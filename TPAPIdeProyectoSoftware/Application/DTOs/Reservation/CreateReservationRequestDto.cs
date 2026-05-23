namespace Application.DTOs.Reservation
{
    public class CreateReservationRequestDto
    {
        public int UserId { get; set; }
        public Guid SeatId { get; set; } 


    }
}