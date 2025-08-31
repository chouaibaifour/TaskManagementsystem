using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Tasks.Dtos;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;

namespace TaskManagement.Application.Tasks.Abstractions
{
    public  interface ITaskService
    {
        Task<Result<TaskDto>> CreateTaskAsync(CreateTaskDto dto);
        Task<Result<TaskDto>> GetTaskByIdAsync(TaskId taskId);
        Task<Result<List<TaskDto>>> GetTasksByProjectIdAsync(ProjectId projectId);
        Task<Result<bool>> DeleteTaskAsync(TaskId taskId);
        Task<Result<TaskDto>> UpdateTaskAsync(UpdateTaskDto dto);
        Task<Result<TaskDto>> SetLowProirity(TaskId taskId);
        Task<Result<TaskDto>> SetMediumPriority(TaskId taskId);
        Task<Result<TaskDto>> SetHighPriority(TaskId taskId);
        Task<Result<TaskDto>> CompleteTask(TaskId taskId);
        Task<Result<TaskDto>> StartTask(TaskId taskId);
        Task<Result<TaskDto>> ReopenTask(TaskId taskId);
        Task<Result<TaskDto>> UpdateDueDate(TaskId taskId, DateTime dueDate);


    }
}
