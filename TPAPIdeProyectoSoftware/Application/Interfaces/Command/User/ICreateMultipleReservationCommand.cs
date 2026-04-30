using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command.User
{
    public interface ICreateMultipleReservationCommand
    {
        Task Execute(List<RESERVATION> reservations);
    }
}
