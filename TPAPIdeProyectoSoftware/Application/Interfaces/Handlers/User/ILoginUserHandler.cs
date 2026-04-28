using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.User
{
    public interface ILoginUserHandler
    {
        Task<(bool Success, string Message, int? UserId, string? Username)> Handle(LoginRequest request);
    }
}
