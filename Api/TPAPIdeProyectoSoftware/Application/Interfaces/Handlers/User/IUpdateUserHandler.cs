using Application.DTOs.User;
using System;

namespace Application.Interfaces.Handlers.User
{
    public interface IUpdateUserHandler
    {
        Task<string> Handle(UpdateUserCommand command);
    }
}
