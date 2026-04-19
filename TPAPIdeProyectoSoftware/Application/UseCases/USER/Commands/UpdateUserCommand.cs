using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Commands
{
    public class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly IUserRepository _repository;

        public UpdateUserCommand(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteUpdateUser(IdUserRequestDto dto)
        {
            
        }
    }
}