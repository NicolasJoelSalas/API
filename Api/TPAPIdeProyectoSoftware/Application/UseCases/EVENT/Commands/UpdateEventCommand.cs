using System;


namespace Application.UseCases.EVENT.Commands
{
    public class UpdateEventCommand 
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime EventDate { get; }
        public string Venue { get; }
        public string Status { get; }

        public UpdateEventCommand(
            int id,
            string name,
            DateTime eventDate,
            string venue,
            string status)
        {
            Id = id;
            Name = name;
            EventDate = eventDate;
            Venue = venue;
            Status = status;
        }
    }
}