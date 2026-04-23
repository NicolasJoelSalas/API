using Application.Interfaces.Queries.Event;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EVENT.Queries
{
    public class NameExistsEventQuery : INameExistsEventQuery
    {
        private readonly IEventRepository _eventRepository;

        public NameExistsEventQuery(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> NameExistsEventHandle(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return await _eventRepository.NameExistsAsync(name);
        }



    }
}