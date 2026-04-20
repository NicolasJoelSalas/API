using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;

namespace Application
{
    public class EmailExistsUserQuery : IEmailExistsUserquery
    {
        private readonly IUserRepository _repository;

        public EmailExistsUserQuery(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _repository.EmailExistsAsync(email);
        }
    }
}