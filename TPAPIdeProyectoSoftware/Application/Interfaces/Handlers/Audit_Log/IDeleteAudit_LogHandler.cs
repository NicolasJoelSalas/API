using Application.DTOs.User;
using Application.UseCases.USER.Commands;
using System;

namespace Application.Interfaces.Handlers.User
{
    public interface IDeleteAudit_LogHandler
    {
        Task<string> Handle(DeleteAudit_LogCommand command);
    }
}
