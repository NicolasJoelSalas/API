using Application.DTOs.User;
using Application.UseCases.USER.Queries;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetByIdUserHandler
    {
        Task<(UserResponseDto? user, string message)> Handle(GetByIdUserQuery query);

    }
}
