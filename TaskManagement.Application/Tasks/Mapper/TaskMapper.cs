using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Tasks.Contracts;

namespace TaskManagement.Application.Tasks.Mapper
{
    public static class TaskMapper
    {
        public static TaskResponse ToDto(this Domain.Tasks.Task task)
        {
            return new TaskResponse(
                task.Id,
                task.Title.ToString(),
                task.Description.ToString(),
                task.TaskStatus.ToEnum(),
                task.TaskPriority.ToEnum(),
                task.DueDate,
                task.CreatedById,
                task.AssignedToId,
                task.ProjectId,
                task.Comments.Select(c => c.ToResponse()).ToList()
                );
        }
    }
}
