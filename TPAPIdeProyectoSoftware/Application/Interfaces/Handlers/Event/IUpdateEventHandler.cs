using Application.DTOs.Event;
using Application.DTOs.User;
using Application.UseCases.EVENT.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Event
{
    public interface IUpdateEventHandler
    {
        Task<string> Handle(UpdateEventCommand command);
    }
}