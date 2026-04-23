using Application.DTOs.Event;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Event
{
    public interface IUpdateEventHandler
    {
        Task<string> UpdateEventHandle(int id, EventResquestDto dto);
    }
}