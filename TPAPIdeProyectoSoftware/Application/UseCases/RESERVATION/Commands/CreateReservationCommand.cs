using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases.USER.Commands
{
    public class CreateReservationCommand 
    {
        public int UserId { get; }
        public Guid SeatId { get; }

        public CreateReservationCommand(
            int userId,
            Guid seatId )
        {
            UserId = userId;
            SeatId = seatId;
        }

    }
}