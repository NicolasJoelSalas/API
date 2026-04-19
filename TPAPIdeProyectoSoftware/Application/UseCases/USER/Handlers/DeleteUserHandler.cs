using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Handlers
{
    public class DeleteUserHandler : IDeleteUserHandler
    {
        private readonly IDeleteUserCommand _command;

        public DeleteUserHandler(IDeleteUserCommand command)
        {
            _command = command;
        }
        public async Task Handle(IdUserRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
