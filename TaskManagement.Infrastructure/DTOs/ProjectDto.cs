namespace TaskManagement.Infrastructure.DTOs;

public class ProjectDto(
    Guid id,
    string name,
    string description,
    Guid ownerId,
    string status,
    DateTime createdAtUtc,
    DateTime? updateAtUtc
    )
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public Guid OwnerId { get; set; } = ownerId;
    public string Status { get; set; } = status;
    public DateTime CreatedAtUtc { get; set; } = createdAtUtc;
    public DateTime? UpdatedAtUtc { get; set; } = updateAtUtc;
}