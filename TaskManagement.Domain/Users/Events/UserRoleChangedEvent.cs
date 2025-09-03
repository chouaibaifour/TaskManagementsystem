
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Users.Events
{
   public sealed record UserRoleChangedEvent
        (UserId UserId , UserRole OldUserRole ,UserRole NewUserRole,string ChangedBy,DateTime OccurredAtUtc);
}
