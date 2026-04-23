using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Commands.Event
{
    public interface IUpdateEventCommand
    {
        Task ExecuteUpdateEvent(EVENT eventEntity);
    }
}