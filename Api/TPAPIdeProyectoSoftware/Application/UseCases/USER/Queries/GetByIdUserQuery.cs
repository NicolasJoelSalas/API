using System;

namespace Application.UseCases.USER.Queries
{
    public class GetByIdUserQuery 
    {
        public int Id { get; }

        public GetByIdUserQuery(int id)
        {
            Id = id;
        }
    }
}