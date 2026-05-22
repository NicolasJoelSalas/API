using Application.DTOs;
using Application.DTOs.User;
using Application.UseCases.USER.Commands;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.User
{
    public interface ICreateAudit_LogHandler
    {
        Task<string> Handle(CreateAudit_LogCommand command);
    }
}
