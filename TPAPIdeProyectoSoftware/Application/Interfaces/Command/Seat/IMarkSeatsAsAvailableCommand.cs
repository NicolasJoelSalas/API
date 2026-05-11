using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command.Seat
{
    public interface IMarkSeatsAsAvailableCommand
    {
        Task Execute(Guid seatId);
    }
}
