using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ObjectValues;
using TaskManagement.Domain.Users.ObjectValues;

namespace TaskManagement.Domain.Projects.Events
{
    public sealed record ProjectMemberRoleChangedEvent
        (ProjectId ProjectId, UserId UserId, MemberRole NewRole, DateTime OccurrdAtUtc);
    
}
