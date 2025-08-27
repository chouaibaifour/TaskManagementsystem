using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ValueObjects;

namespace TaskManagement.Domain.Projects.Events
{
   public sealed record ProjectReActivateEvent(ProjectId ProjectId, DateTime OccurrdAtUtc);

}
