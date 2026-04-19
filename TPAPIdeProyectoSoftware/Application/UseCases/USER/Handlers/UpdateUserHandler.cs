using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Handlers
{
    public class UpdateUserHandler : IUpdateUserHandler
    {
        private readonly IUpdateUserCommand _command;

        public UpdateUserHandler(IUpdateUserCommand command)
        {
            _command = command;
        }
        public Task Handle(IdUserRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
