using System;

namespace Application.UseCases
{
    public class CreateSectorCommand 
    {
        public int EventId { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Capacity { get; }

        public CreateSectorCommand(
            int eventId,
            string name,
            decimal price,
            int capacity)
        {
            EventId = eventId;
            Name = name;
            Price = price;
            Capacity = capacity;
        }

    }
}