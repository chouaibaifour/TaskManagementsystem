using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Tasks.Contracts
{
    public record struct UpdateTaskRequest
    (
         Guid TaskId,
       string NewTitle,
       string NewDescription,
       Guid UpdatedById
    );


}
