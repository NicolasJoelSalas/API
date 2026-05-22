
using System;
namespace Application.UseCases.USER.Queries
{
    public class GetAllUserLoginQuery 
    {
        public string Name { get; }
        public string Password { get; }

        public GetAllUserLoginQuery(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
