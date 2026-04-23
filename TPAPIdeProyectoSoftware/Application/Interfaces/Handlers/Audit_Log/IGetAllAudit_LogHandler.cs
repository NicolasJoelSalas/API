using Application.DTOs;
using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetAllAudit_LogHandler
    {
        Task<(List<Audit_LogResponseDto> Audit_Logs, string message)> Handle();
    }
}
