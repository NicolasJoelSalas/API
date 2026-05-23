using Domain.Entities;
using System;

namespace Application.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<List<EVENT>> GetAllAsync();
        Task AddAsync(EVENT eventEntity);

        Task<EVENT> GetByIdAsync(int id);

        Task UpdateAsync(EVENT eventEntity);

        Task DeleteAsync(EVENT eventEntity);

        Task<bool> NameExistsAsync(string name);

    }
}