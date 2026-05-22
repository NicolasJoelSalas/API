using System;


namespace Application.UseCases.EVENT.Queries
{
    public class GetByIdEventQuery 
    {
        public int Id { get; }

        public GetByIdEventQuery(int id)
        {
            Id = id;
        }

    }
}