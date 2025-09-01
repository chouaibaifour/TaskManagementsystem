using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Tasks.Contracts.Comment;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.Events.NewFolder;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Tasks.Contracts
{
    public record struct TaskResponse
        (
            TaskId Id,
            string Title,
            string Description,
            string Status,
            string Priority,
            DateTime DueDate,
            UserId CreatedById,
            UserId AssignedToId,
            ProjectId ProjectId,
            List<CommentResponse> Comments
            
        );
}
