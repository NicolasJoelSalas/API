using System;

namespace Application
{
    public class UpdateUserCommand
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string PasswordHash { get; }

        public UpdateUserCommand(
            int id,
            string name,
            string email,
            string passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }

}