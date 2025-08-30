using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Users.Dtos
{
    public record struct UserRegisterDto
    (
        string FirstName,
        string LastName,
        string Email,
        string Password);
    
}
