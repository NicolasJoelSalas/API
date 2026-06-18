using Application.DTOs.User;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(USER user);

        Task<USER> GetByIdAsync(int id);

        Task<List<USER>> GetAllAsync();

        Task UpdateAsync(USER user);

        Task DeleteAsync(USER user);

        Task<bool> EmailExistsAsync(string email);
    }
}
