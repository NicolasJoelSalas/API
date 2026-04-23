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
    public interface IGetAllAudit_LogQuery
    {
        Task <List<Audit_LogResponseDto>> GetAll();
    }
}
