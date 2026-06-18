using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SECTOR
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Capacity { get; set; }

        public List<SEAT>? SEATS { get; set; }

        public EVENT EVENT { get; set; }
    }
}
