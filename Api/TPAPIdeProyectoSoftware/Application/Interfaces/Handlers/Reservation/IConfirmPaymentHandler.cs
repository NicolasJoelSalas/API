using System;

namespace Application.Interfaces.Handlers.Reservation
{
    public interface IConfirmPaymentHandler
    {
        Task<string> Handle(List<Guid> seatIds);
    }
}
