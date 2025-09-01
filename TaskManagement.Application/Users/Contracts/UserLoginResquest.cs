using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Users.Contracts
{
    public record struct UserLoginResquest(
        string Email,
        string Password);
    
    
}
