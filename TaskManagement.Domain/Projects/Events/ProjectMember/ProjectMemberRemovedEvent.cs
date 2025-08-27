
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Projects.Events.ProjectMember
{
    public sealed record ProjectMemberRemovedEvent

        (ProjectId ProjectId, UserId UserId, DateTime OccurrdAtUtc);

    
}
