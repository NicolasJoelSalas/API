using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.User
{
    public interface ICreateAudit_LogHandler
    {
        Task<string> Handle(Audit_LogRequestDto dto);
    }
}
