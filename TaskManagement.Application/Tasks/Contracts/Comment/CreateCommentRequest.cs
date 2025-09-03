
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Tasks.Contracts.Comment
{
    public record struct CreateCommentRequest   
    (
        TaskId TaskId,
        string CommentContent, 
        UserId UserId
    );
}
