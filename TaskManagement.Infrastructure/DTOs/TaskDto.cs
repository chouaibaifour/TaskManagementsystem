namespace TaskManagement.Infrastructure.DTOs;

public class TaskDto
    (
        Guid id,
        string title,
        string description,
        Enum status,
        Enum priority,
        DateTime dueDate,
        Guid createdById,
        Guid assignedToId,
        Guid projectId,
        DateTime createdAtUtc,
        DateTime? updateAtUtc,
        List<CommentDto> comments
    )
{
    public Guid Id { get; init; } = id;
    public string Title { get; init; } = title;
    public string Description { get; init; }= description;
    public Enum Status { get; init; }= status;
    public Enum Priority { get; init; }= priority;
    public DateTime DueDate { get; init; }= dueDate;
    public Guid CreatedById { get; init; }= createdById;
    public Guid AssignedToId { get; init; }= assignedToId;
    public Guid ProjectId { get; init; }= projectId;
    public DateTime CreatedAtUtc { get; init; }= createdAtUtc;
    public DateTime? UpdateAtUtc { get; init; } = updateAtUtc;
    public List<CommentDto> Comments { get; init; } = comments;
}