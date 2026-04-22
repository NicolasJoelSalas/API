using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command
{
    public interface IDeleteSeatCommand
    {
        Task ExecuteDeleteSeat(Guid id);
    }
}
