using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskStatus = TaskManagement.Domain.Tasks.ValueObjects.TaskStatus;

namespace TaskManagement.Domain.Tasks.Events
{
    public sealed record TaskStatusChangedEvent(TaskId TaskId,TaskStatus OldTaskStatus,TaskStatus NewTaskStatus);
    
    
}
