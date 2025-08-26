using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Users
{
    public sealed record UserDto
    ( 
        Guid Id,
        string FullName,
        string Email,
        string Role
    );
       
    
}
