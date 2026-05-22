using System;


namespace Application.UseCases.EVENT.Commands
{
    public class CreateEventCommand 
    {
        public string Name { get; }
        public DateTime EventDate { get; }
        public string Venue { get; }
        public string Status { get; }

        public CreateEventCommand(
            string name,
            DateTime eventDate,
            string venue,
            string status)
        {
            Name = name;
            EventDate = eventDate;
            Venue = venue;
            Status = status;
        }
    }
}