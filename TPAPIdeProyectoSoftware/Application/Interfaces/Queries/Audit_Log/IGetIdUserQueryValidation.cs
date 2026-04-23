using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries.Audit_Log
{
    public interface IGetIdUserQueryValidation
    {
        Task<UserResponseDto> GetById(int? id);
    }
}
