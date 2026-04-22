using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application
{
    public class UpdateAudit_LogCommand : IUpdateAudit_LogCommand
    {
        private readonly IAudit_LogRepository _repository;

        public UpdateAudit_LogCommand(IAudit_LogRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteUpdateAudit_Log(AUDIT_LOG user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}