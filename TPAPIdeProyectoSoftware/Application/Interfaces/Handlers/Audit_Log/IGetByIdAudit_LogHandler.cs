using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetByIdAudit_LogHandler
    {
        Task<(Audit_LogResponseDto Audit_Logs, string message)> Handle(Guid id);

    }
}
