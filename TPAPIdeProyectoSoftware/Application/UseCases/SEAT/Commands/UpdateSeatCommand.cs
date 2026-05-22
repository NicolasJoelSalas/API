using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application
{
    public class UpdateSeatCommand 
    {
        public Guid Id { get; }
        public int SectorId { get; }
        public string RowIdentifier { get; }
        public int SeatNumber { get; }
        public string Status { get; }
        public int Version { get; }

        public UpdateSeatCommand(
            Guid id,
            int sectorId,
            string rowIdentifier,
            int seatNumber,
            string status,
            int version)
        {
            Id = id;
            SectorId = sectorId;
            RowIdentifier = rowIdentifier;
            SeatNumber = seatNumber;
            Status = status;
            Version = version;
        }
    }
}