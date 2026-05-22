using System;


namespace Application.UseCases.EVENT.Commands
{
    public class CreateEventCommand 
    {
        public string Name { get; }
        public DateTime EventDate { get; }
        public string Venue { get; }

        public CreateEventCommand(
            string name,
            DateTime eventDate,
            string venue)
        {
            Name = name;
            EventDate = eventDate;
            Venue = venue;
        }
    }
}