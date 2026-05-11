using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command.Reservation
{
    public interface IUpdateReservationStatusExpiredCommand
    {
        Task ExecuteUpdateReservation(Domain.Entities.RESERVATION reser, string status);
    }
}
