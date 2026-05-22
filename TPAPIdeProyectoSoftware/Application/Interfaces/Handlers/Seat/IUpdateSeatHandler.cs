using Application.DTOs.User;
using System;

namespace Application.Interfaces.Handlers.User
{
    public interface IUpdateSeatHandler
    {
        Task<string> Handle(UpdateSeatCommand command);
    }
}
