using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Projects.Events
{
    public sealed record ProjectMemberRemovedEvent
        (Guid ProjectId, Guid UserId, DateTime OccurrdAtUtc);
    
}
