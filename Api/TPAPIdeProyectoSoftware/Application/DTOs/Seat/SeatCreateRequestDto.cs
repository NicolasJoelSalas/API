using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class SeatCreateRequestDto
    {
        public int SectorId { get; set; }
        public string RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        
        public int Version { get; set; }
    }
}
