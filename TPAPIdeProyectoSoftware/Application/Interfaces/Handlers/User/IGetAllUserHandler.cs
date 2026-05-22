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
    public interface IGetAllUserHandler
    {
        Task<(List<UserResponseDto> users, string message)> Handle(GetAllUserQuery query);
    }
}
