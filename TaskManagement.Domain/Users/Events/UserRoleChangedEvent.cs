using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Users.Enums;
using TaskManagement.Domain.Users.ObjectValues;

namespace TaskManagement.Domain.Users.Events
{
   public sealed record UserRoleChangedEvent
        (UserId UserId , Role OldRole ,Role NewRole,string ChangedBy,DateTime OccurredAtUtc);
}
