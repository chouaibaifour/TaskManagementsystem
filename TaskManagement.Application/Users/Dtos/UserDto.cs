using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.Dtos
{
    public sealed record UserDto
    ( 
        UserId Id,
        string FullName,
        string Email,
        string Role
    );
       
    
}
