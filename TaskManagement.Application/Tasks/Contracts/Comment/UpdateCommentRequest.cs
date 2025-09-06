
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Tasks.Contracts.Comment
{
    public record struct UpdateCommentRequest
        (
            Guid TaskId,
            Guid CommentId,
            string NewContent,
            Guid UserId
        );

}
