using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;

namespace Application.UseCases.USER.Handlers
{
    public class DeleteAudit_LogHandler : IDeleteAudit_LogHandler
    {
        private readonly IDeleteAudit_LogCommand _command;
        private readonly IGetByIdAudit_LogQuery _query;

        public DeleteAudit_LogHandler(
            IDeleteAudit_LogCommand command,
            IGetByIdAudit_LogQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<string> Handle(Guid id)
        {
            var user = await _query.GetById(id);

            if (user == null)
                return "Audit_Log no encontrado";

            await _command.ExecuteDeleteAudit_Log(id);

            return "Audit_Log eliminado correctamente";
        }
    }
}