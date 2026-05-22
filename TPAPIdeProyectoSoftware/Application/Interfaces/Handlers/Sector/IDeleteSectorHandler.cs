using Application.DTOs.User;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers
{
    public interface IDeleteSectorHandler
    {
        Task<string> Handle(DeleteSectorCommand command);
    }
}
