using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Commands
{
    public class CreateAudit_LogCommand : ICreateAudit_LogCommand
    {
        private readonly IAudit_LogRepository _repository;

        public CreateAudit_LogCommand(IAudit_LogRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteCreateAudit_Log(Domain.Entities.AUDIT_LOG user)
        {
            await _repository.AddAsync(user);
        }

    }
}