using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command.Reservation
{
    public interface IUpdateReservationsStatusCommand
    {
        Task Execute(List<Guid> reservationIds, string status);
    }
}
