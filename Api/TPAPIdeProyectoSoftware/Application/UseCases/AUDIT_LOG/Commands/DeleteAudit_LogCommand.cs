using System;

namespace Application.UseCases.USER.Commands
{
    public class DeleteAudit_LogCommand
    {

        public Guid Id { get; }

        public DeleteAudit_LogCommand(Guid id)
        {
            Id = id;
        }

    }
}