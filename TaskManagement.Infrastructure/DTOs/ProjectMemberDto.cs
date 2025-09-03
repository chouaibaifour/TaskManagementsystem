using TaskManagement.Domain.Tasks.ValueObjects;

namespace TaskManagement.Infrastructure.DTOs;

public class ProjectMemberDto(
    Guid userId, 
    Enum memberRole, 
    DateTime joinedAtUtc,
    bool isActive, 
    List<Guid> assignedTaskIds
)
{
    public Guid UserId { get; init; } = userId;
    public Enum MemberRole { get; init; } = memberRole;
    public DateTime JoinedAtUtc { get; init; } = joinedAtUtc;
    public bool IsActive { get; init; } = isActive;
    public List<Guid> AssignedTaskIds { get; init; } = assignedTaskIds;
}
