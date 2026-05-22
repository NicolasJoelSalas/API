using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Commands
{
    public class CreateSeatCommand 
    {
        public int SectorId { get; }
        public string RowIdentifier { get; }
        public int SeatNumber { get; }
        public int Version { get; }

        public CreateSeatCommand(
            int sectorId,
            string rowIdentifier,
            int seatNumber,
            int version)
        {
            SectorId = sectorId;
            RowIdentifier = rowIdentifier;
            SeatNumber = seatNumber;
            Version = version;
        }

    }
}