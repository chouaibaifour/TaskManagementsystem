
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Tasks.Events
{
    public sealed record CommentAddedEvent
        (TaskId TaskId,UserId AuthorId ,CommentId CommentId, DateTime OccurredAtUtc);

}
