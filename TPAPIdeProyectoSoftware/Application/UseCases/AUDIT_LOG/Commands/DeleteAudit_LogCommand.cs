using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;

namespace Application.UseCases.USER.Commands
{
    public class DeleteAudit_LogCommand : IDeleteAudit_LogCommand
    {
        private readonly IAudit_LogRepository _repository;

        public DeleteAudit_LogCommand(IAudit_LogRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteDeleteAudit_Log(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}