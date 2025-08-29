using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Tasks.Events.NewFolder
{
    public sealed record CommentEditedEvent
        (TaskId TaskId, CommentId CommentId,UserId UserId, DateTime OccurredAtUtc);

}
