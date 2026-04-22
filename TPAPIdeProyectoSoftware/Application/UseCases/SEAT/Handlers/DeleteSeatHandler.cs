using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;

namespace Application.UseCases
{
    public class DeleteSeatHandler : IDeleteSeatHandler
    {
        private readonly IDeleteSeatCommand _command;
        private readonly IGetByIdSeatQuery _query;

        public DeleteSeatHandler(
            IDeleteSeatCommand command,
            IGetByIdSeatQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<string> Handle(Guid id)
        {
            var user = await _query.GetById(id);

            if (user == null)
                return "Seat no encontrado";

            await _command.ExecuteDeleteSeat(id);

            return "Seat eliminado correctamente";
        }
    }
}