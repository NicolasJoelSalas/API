using Application.DTOs.Event;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries.Event
{
    public interface IGetAllEventQuery
    {
        Task<List<EventResponseDto>> GetAllEvent();
    }
}