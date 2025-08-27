using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Tasks.Events
{
   public sealed record CommentDeletedEvent
        (TaskId TaskId, UserId CommentId, UserId DeletedById ,DateTime OccurredAtUtc);

}
