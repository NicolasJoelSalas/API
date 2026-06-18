using Application.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Application
{
    public class UpdateSectorCommand 
    {
        public int Id { get; }
        public int EventId { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Capacity { get; }

        public UpdateSectorCommand(
            int id,
            int eventId,
            string name,
            decimal price,
            int capacity)
        {
            Id = id;
            EventId = eventId;
            Name = name;
            Price = price;
            Capacity = capacity;
        }
    }
}