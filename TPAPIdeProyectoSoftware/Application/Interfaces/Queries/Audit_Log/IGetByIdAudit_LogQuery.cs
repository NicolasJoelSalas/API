using Application.DTOs;
using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries.User
{
    public interface IGetByIdAudit_LogQuery
    {
        Task <Audit_LogResponseDto> GetById(Guid id);
    }
}
