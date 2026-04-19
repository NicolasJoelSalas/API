using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Commands
{
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly IUserRepository _repository;

        public DeleteUserCommand(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteDeleteUser(IdUserRequestDto user)
        {
            var userold = await _repository.GetByIdAsync(user.Id);
            await _repository.DeleteAsync(user.Id);

        }
    }
}
