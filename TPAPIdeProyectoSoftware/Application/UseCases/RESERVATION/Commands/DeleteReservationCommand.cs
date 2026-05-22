using Application.Interfaces.Repositories;

namespace Application.UseCases.USER.Commands
{
    public class DeleteReservationCommand 
    {
        public Guid Id { get; }

        public DeleteReservationCommand(Guid id)
        {
            Id = id;
        }
    }
}