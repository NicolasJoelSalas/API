using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Repositories;  

namespace Application.UseCases
{
    public class GetByIdSectorQuery 
    {
        public int Id { get; }

        public GetByIdSectorQuery(int id)
        {
            Id = id;
        }
    }
}