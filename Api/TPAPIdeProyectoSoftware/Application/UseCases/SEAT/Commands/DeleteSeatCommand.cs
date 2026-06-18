using Application.Interfaces.Repositories;

namespace Application.UseCases.USER.Commands
{
    public class DeleteSeatCommand 
    {
        public Guid Id { get; }

        public DeleteSeatCommand(Guid id)
        {
            Id = id;
        }
    }
}