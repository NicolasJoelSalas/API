using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command
{
    public interface IDeleteAudit_LogCommand
    {
        Task ExecuteDeleteAudit_Log(Guid id);
    }
}
