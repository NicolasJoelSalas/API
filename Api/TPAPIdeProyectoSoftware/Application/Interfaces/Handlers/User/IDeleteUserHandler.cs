using Application.UseCases.USER.Commands;
using System;

namespace Application.Interfaces.Handlers.User
{
    public interface IDeleteUserHandler
    {
        Task<string> Handle(DeleteUserCommand command);
    }
}
