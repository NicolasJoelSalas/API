
using System;

namespace Application.UseCases.USER.Queries
{
    public class GetByIdSeatQuery 
    {
        public Guid Id { get; }

        public GetByIdSeatQuery(Guid id)
        {
            Id = id;
        }
    }
}