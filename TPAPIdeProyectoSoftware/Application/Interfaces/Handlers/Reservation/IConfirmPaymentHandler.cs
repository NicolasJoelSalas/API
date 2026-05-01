using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Reservation
{
    public interface IConfirmPaymentHandler
    {
        Task<string> Handle(List<Guid> seatIds);
    }
}
