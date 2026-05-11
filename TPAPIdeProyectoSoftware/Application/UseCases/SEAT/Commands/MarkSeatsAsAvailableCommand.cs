using Application.Interfaces.Command.Seat;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.SEAT.Commands
{
    public class MarkSeatsAsAvailableCommand : IMarkSeatsAsAvailableCommand
    {
        private readonly ISeatRepository _seatRepository;

        public MarkSeatsAsAvailableCommand(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task Execute(Guid seatId)
        {
            await _seatRepository.MarkAsAvailableAsync(seatId);
        }
    }
}
