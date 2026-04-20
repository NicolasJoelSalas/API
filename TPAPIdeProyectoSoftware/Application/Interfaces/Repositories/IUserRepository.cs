using Application.DTOs.User;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IQueryable<USER> Query();
        Task AddAsync(USER user);

        Task<USER> GetByIdAsync(int id);

        Task UpdateAsync(USER user);

        Task DeleteAsync(int id);

        Task<bool> EmailExistsAsync(string email);
    }
}
