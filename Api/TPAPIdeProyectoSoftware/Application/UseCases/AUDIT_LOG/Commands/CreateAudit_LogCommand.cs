using System;

namespace Application.UseCases.USER.Commands
{
    public class CreateAudit_LogCommand 
    {
        public int? UserId { get; }
        public string Action { get; }
        public string EntityType { get; }
        public string EntityId { get; }
        public string Details { get; }

        public CreateAudit_LogCommand(
            int? userId,
            string action,
            string entityType,
            string entityId,
            string details)
        {
            UserId = userId;
            Action = action;
            EntityType = entityType;
            EntityId = entityId;
            Details = details;
        }

    }
}