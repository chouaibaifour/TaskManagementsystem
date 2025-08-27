using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Projects.Events.ProjectMember
{
    public sealed record ProjectMemberDesActivateEvent(
        ProjectId ProjectId, UserId UserId, DateTime OccurrdAtUtc);
    
}
