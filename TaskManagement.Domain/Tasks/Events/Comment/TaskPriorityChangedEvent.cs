using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Tasks.ValueObjects;

namespace TaskManagement.Domain.Tasks.Events.Comment
{
    public sealed record TaskPriorityChangedEvent
        (TaskId TaskId, Priority OldPriority, Priority NewPriority, DateTime OccurredAtUtc);


}
