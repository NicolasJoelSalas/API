using Application.Interfaces.Repositories;

namespace Application.UseCases
{
    public class DeleteSectorCommand 
    {
        public int Id { get; }

        public DeleteSectorCommand(int id)
        {
            Id = id;
        }
    }
}