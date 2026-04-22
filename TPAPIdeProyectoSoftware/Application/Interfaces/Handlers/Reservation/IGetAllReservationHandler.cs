using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.User
{
    public interface IGetAllReservationHandler
    {
        Task<(List<ReservationResponseDto> Reservation, string message)> Handle();
    }
}
