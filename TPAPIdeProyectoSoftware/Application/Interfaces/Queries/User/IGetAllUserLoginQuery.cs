using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries.User
{
    public interface IGetAllUserLoginQuery
    {
        Task<List<UserResponseDto>> GetAll();
    }
}
