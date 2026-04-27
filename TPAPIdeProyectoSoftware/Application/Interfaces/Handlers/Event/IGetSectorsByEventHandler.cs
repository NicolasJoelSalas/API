using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Handlers.Event
{
    public interface IGetSectorsByEventHandler 
    {
        Task<(List<SectorResponseDto>, string)> Handle(int eventId);
    }
}
