using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Tasks.Dtos
{
    public record struct CreateTaskDto(
            string title,
            string description,
            ProjectId projectId,
            UserId createdById,
            UserId? assignedToId,
            byte priority,
            DateTime dueDate);
    
    
}
