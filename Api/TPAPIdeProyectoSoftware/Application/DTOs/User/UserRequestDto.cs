using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
