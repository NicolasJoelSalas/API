using Application.DTOs;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.User
{
    public interface IUpdateAudit_LogHandler
    {
        Task<string> Handle(Guid id, Audit_LogRequestDto dto);
    }
}
