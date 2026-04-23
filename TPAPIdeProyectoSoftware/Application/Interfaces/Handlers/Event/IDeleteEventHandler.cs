using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Event
{
    public interface IDeleteEventHandler
    {
        Task<string> DeleteEventHandle(int id);
    }
}