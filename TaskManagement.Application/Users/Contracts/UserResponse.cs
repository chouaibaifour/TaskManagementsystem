using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.Contracts
{
    public  record struct UserResponse
    ( 
        UserId Id,
        string FullName,
        string Email,
        string Role
    );
       
    
}
