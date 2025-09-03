namespace TaskManagement.Infrastructure.DTOs;

public class CommentDto
    (
        Guid id,
        Guid authorId,
        string content,
        DateTime createdAtUtc,
        DateTime? updatedAtUtc
    )
{
    public Guid Id { get; init; } = id;
    public Guid AuthorId { get; init; }= authorId;
    public string Content { get; init; }= content;
    public DateTime CreatedAtUtc { get; init; }= createdAtUtc;
    public DateTime? UpdatedAtUtc { get; init; }= updatedAtUtc;
    
}