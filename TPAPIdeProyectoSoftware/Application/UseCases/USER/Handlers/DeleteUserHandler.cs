using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;

namespace Application.UseCases.USER.Handlers
{
    public class DeleteUserHandler : IDeleteUserHandler
    {
        private readonly IDeleteUserCommand _command;
        private readonly IGetByIdUserQuery _query;

        public DeleteUserHandler(
            IDeleteUserCommand command,
            IGetByIdUserQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<string> Handle(int id)
        {
            var user = await _query.GetById(id);

            if (user == null)
                return "Usuario no encontrado";

            await _command.ExecuteDeleteUser(id);

            return "Usuario eliminado correctamente";
        }
    }
}