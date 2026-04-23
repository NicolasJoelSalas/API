using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries.Event
{
    public interface INameExistsEventQuery
    {
        Task<bool> NameExistsEventHandle(string name);
    }
}