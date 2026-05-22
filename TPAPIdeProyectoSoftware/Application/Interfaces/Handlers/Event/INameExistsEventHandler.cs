using Application.UseCases.EVENT.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Event
{
    public interface INameExistsEventHandler
    {
        Task<bool> Handle(NameExistsEventQuery query);
    }
}
