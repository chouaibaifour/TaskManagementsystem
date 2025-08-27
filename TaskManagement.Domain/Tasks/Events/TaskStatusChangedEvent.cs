using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Tasks.ValueObjects;

namespace TaskManagement.Domain.Tasks.Events
{
    public sealed record TaskStatusChangedEvent(TaskId TaskId,Status OldStatus,Status NewStatus);
    
    
}
