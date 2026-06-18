using System;


namespace Application.UseCases.EVENT.Queries
{
    public class NameExistsEventQuery
    {
        public string Name { get; }

        public NameExistsEventQuery(string name)
        {
            Name = name;
        }

    }
}