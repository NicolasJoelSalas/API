using Application.Interfaces.Commands.Event;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Queries.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EVENT.Commands
{
    public class DeleteEventHandler : IDeleteEventHandler
    {
        private readonly IDeleteEventCommand _command;
        private readonly IGetAllByIdEventQuery _getByIdEventQuery;

        public DeleteEventHandler(IDeleteEventCommand command, IGetAllByIdEventQuery getByIdEventQuery)
        {
            _command = command;
            _getByIdEventQuery = getByIdEventQuery;
        }

        public async Task<string> DeleteEventHandle(int id)
        {
            var existingEvent = await _getByIdEventQuery.GetById(id);

            if (existingEvent == null)
                return "El evento no existe";

            await _command.ExecuteDeleteEvent(id);
            return "El evento se ha eliminado correctamente";
        }
    }
}