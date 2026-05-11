using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries.Reservation
{
    public interface IGetAllByIdDeleteQuery
    {
        Task<List<ReservationResponseDto>> GetAll();
    }
}
