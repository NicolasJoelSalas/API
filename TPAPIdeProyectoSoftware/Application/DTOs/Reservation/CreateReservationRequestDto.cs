namespace Application.DTOs.Reservation
{
    public class CreateReservationRequestDto
    {
        public int UserId { get; set; }
        public List<Guid> SeatIds { get; set; } 


    }
}