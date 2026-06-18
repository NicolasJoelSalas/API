using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class SeatRequestDto
    {
        public int SectorId { get; set; }
        public string RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        public string Status { get; set; }
        public int Version { get; set; }
    }
}
