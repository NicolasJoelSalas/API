using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.User;

namespace Application.UseCases
{
    public class DeleteSectorHandler : IDeleteSectorHandler
    {
        private readonly IDeleteSectorCommand _command;
        private readonly IGetByIdSectorQuery _query;

        public DeleteSectorHandler(
            IDeleteSectorCommand command,
            IGetByIdSectorQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<string> Handle(int id)
        {
            var user = await _query.GetById(id);

            if (user == null)
                return "Sector no encontrado";

            await _command.ExecuteDeleteSector(id);

            return "Sector eliminado correctamente";
        }
    }
}