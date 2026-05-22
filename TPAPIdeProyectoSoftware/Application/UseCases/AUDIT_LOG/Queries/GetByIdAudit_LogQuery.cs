
using System;

namespace Application.UseCases.USER.Queries
{
    public class GetByIdAudit_LogQuery
    {
        public Guid Id { get; }

        public GetByIdAudit_LogQuery(Guid id)
        {
            Id = id;
        }
    }
}