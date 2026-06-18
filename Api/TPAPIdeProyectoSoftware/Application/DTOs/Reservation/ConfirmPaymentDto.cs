using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Reservation
{
    public class ConfirmPaymentDto
    {
        public List<Guid> ReservationIds { get; set; } = new();
    }
}
