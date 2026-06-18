using System;

namespace Application
{
    public class UpdateAudit_LogCommand
    {
        public Guid Id { get; }
        public string Action { get; }
        public string EntityType { get; }
        public string EntityId { get; }
        public string Details { get; }

        public UpdateAudit_LogCommand(
            Guid id,
            string action,
            string entityType,
            string entityId,
            string details)
        {
            Id = id;
            Action = action;
            EntityType = entityType;
            EntityId = entityId;
            Details = details;
        }
    }
}