using Application.DTOs;
using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handler
{
    public interface ICreateSectorHandler
    {
        Task<string> Handle(SectorRequestDto dto);
    }
}
