using Application.DTOs;
using Application.DTOs.User;
using System;

namespace Application.Interfaces.Handlers
{
    public interface IUpdateSectorHandler
    {
        Task<string> Handle(UpdateSectorCommand command);
    }
}
