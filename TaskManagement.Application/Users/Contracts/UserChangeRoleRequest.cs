using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.Contracts
{
    public record struct UserChangeRoleRequest(Guid UserId, Enum NewUserRole, Guid ChangedBy);
    
}
