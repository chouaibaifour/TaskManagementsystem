using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Domain.Tasks.Events
{
    public sealed record TaskCreatedEvent(TaskId TaksId,ProjectId Project,UserId CreatedById);
  
   
}
