namespace TaskManagement.Infrastructure.DTOs;

public class ProjectDto(
    Guid id,
    string name,
    string description,
    Guid ownerId,
    string status,
    DateTime createdAtUtc,
    DateTime? updateAtUtc,
    List<ProjectMemberDto> members
    )
{
    public Guid Id { get; init; } = id;
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
    public Guid OwnerId { get; init; } = ownerId;
    public string Status { get; init; } = status;
    public DateTime CreatedAtUtc { get; init; } = createdAtUtc;
    public DateTime? UpdatedAtUtc { get; init; } = updateAtUtc;
    public List<ProjectMemberDto> Members { get; init; } = members;
}