using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ObjectValues;

namespace TaskManagement.Domain.Projects.Events
{
   public sealed record ProjectCompletedEvent(ProjectId ProjectId,DateTime nowUtc);
    
}
