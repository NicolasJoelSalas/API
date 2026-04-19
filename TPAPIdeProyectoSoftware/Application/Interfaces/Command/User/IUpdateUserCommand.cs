using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command.User
{
    public interface IUpdateUserCommand
    {
        Task ExecuteUpdateUser(IdUserRequestDto user);
    }
}
