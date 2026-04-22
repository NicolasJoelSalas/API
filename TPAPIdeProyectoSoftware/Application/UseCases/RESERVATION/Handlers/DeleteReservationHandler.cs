using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;

namespace Application.UseCases
{
    public class DeleteReservationHandler : IDeleteReservationHandler
    {
        private readonly IDeleteReservationCommand _command;
        private readonly IGetByIdReservationQuery _query;

        public DeleteReservationHandler(
            IDeleteReservationCommand command,
            IGetByIdReservationQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<string> Handle(Guid id)
        {
            var user = await _query.GetById(id);

            if (user == null)
                return "Reservation no encontrado";

            await _command.ExecuteDeleteReservation(id);

            return "Reservation eliminado correctamente";
        }
    }
}