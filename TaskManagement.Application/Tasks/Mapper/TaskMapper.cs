using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Tasks.Mapper
{
    public static class TaskMapper
    {
        public static Dtos.TaskDto ToDto(this Domain.Tasks.Task task)
        {
            return new Dtos.TaskDto(
                task.Id,
                task.Title.Display,
                task.Description.Display,
                task.Status.Display,
                task.Priority.Display,
                task.DueDate,
                task.CreatedById,
                task.AssignedToId,
                task.ProjectId,
                task.Comments.Select(c => c.ToDto()).ToList()
                );
        }
    }
}
