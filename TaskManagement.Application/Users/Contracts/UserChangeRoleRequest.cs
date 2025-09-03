using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Users.Contracts
{
    public record struct UserChangeRoleRequest(UserId UserId, UserRole NewUserRole, string ChangedBy);
    
}
