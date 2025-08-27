using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Tasks.Events
{
    public sealed record CommentEditedEvent(Guid TaskId, Guid CommentId, DateTime OccurredAtUtc);

}
