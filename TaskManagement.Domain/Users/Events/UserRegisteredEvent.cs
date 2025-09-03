
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Users.Events
{
    public sealed record UserRegisteredEvent
        (UserId UserId,string Email,string Name,UserRole UserRole,DateTime OccuredAtUtc);
    
}
