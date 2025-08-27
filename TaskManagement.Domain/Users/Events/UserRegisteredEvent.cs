using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Users.Enums;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Users.Events
{
    public sealed record UserRegisteredEvent
        (UserId UserId,string Email,string Name,Role Role,DateTime OccuredAtUtc);
    
}
