using Application.DTOs;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers
{
    public interface IUpdateSectorHandler
    {
        Task<string> Handle(UpdateSectorCommand command);
    }
}
