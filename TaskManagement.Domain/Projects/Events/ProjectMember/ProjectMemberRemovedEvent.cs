using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD
using TaskManagement.Domain.Projects.ObjectValues;
using TaskManagement.Domain.Users.ObjectValues;
=======
>>>>>>> dc5a56ad8758bf8e00faf6b3692e0580510c7134

namespace TaskManagement.Domain.Projects.Events.ProjectMember
{
    public sealed record ProjectMemberRemovedEvent
<<<<<<< HEAD
        (ProjectId ProjectId, UserId UserId, DateTime OccurrdAtUtc);
=======
        (Guid ProjectId, Guid UserId, DateTime OccurrdAtUtc);
>>>>>>> dc5a56ad8758bf8e00faf6b3692e0580510c7134
    
}
