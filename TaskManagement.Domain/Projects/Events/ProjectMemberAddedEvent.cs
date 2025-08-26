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

namespace TaskManagement.Domain.Projects.Events
{
    public sealed record ProjectMemberAddedEvent
<<<<<<< HEAD
        (ProjectId ProjectId, UserId UserId,MemberRole Role, DateTime OccurrdAtUtc);
=======
        (Guid ProjectId, Guid UserId, DateTime OccurrdAtUtc);
>>>>>>> dc5a56ad8758bf8e00faf6b3692e0580510c7134
    
}
