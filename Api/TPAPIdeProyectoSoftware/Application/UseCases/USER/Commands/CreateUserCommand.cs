using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCases.USER.Commands
{
    public class CreateUserCommand 
    {
        public string Name { get; }
        public string Email { get; }
        public string PasswordHash { get; }

        public CreateUserCommand(
            string name,
            string email,
            string passwordHash) 
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

    }
}