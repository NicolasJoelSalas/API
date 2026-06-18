using System;
namespace Application.UseCases.EVENT.Commands
{
    public class DeleteEventCommand 
    {
        public int Id { get; }

        public DeleteEventCommand(int id)
        {
            Id = id;
        }
    }
}