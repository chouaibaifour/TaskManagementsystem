using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ObjectValues;
using TaskManagement.Domain.Tasks.Value_Objects;
using TaskManagement.Domain.Users.ObjectValues;

namespace TaskManagement.Domain.Projects.Events.ProjectMember
{
    public sealed record ProjectMemberTaskAssignedEvent
        (ProjectId ProjectId, UserId UserId, TaskId TaskId, DateTime nowUtc);
    
}
