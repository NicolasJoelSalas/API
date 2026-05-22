using System;

namespace Application.UseCases.USER.Commands
{
    public class DeleteUserCommand 
    {
        public int Id { get; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}