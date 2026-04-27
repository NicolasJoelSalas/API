using Application.DTOs.User;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetEntitySeatQuery : IGetEntitySeatQuery
    {
        private readonly ISeatRepository _repository;

        public GetEntitySeatQuery (ISeatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.SEAT> GetById(Guid id)
        {
            return await _repository
            .Query()
            .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
