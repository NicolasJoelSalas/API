
using System;

namespace Application.UseCases.USER.Queries
{
    public class GetByIdReservationQuery
    {
        public Guid Id { get; }

        public GetByIdReservationQuery(Guid id)
        {
            Id = id;
        }
    }
}