
using TaskManagement.Application.Common;
using TaskManagement.Application.Tasks.Contracts;
using TaskManagement.Application.Tasks.Contracts.Comment;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;

namespace TaskManagement.Application.Tasks.Abstractions
{
    public  interface ITaskService
    {
        Task<Result<TaskResponse>> CreateTaskAsync(CreateTaskRequest dto);
        Task<Result<TaskResponse>> GetTaskByIdAsync(TaskId taskId);
        Task<Result<List<TaskResponse>>> GetTasksByProjectIdAsync(ProjectId projectId);
        Task<Result<bool>> DeleteTaskAsync(TaskId taskId);
        Task<Result<TaskResponse>> UpdateTaskAsync(UpdateTaskRequest dto);
        Task<Result<TaskResponse>> SetLowPriority(TaskId taskId);
        Task<Result<TaskResponse>> SetMediumPriority(TaskId taskId);
        Task<Result<TaskResponse>> SetHighPriority(TaskId taskId);
        Task<Result<TaskResponse>> CompleteTask(TaskId taskId);
        Task<Result<TaskResponse>> StartTask(TaskId taskId);
        Task<Result<TaskResponse>> ReopenTask(TaskId taskId);
        Task<Result<TaskResponse>> UpdateDueDate(TaskId taskId, DateTime dueDate);
        
        Task<Result<TaskResponse>> AddCommentAsync(CreateCommentRequest dto);
        Task<Result<IEnumerable<CommentResponse>>> GetCommentsByTaskIdAsync(TaskId taskId);
        Task<Result<bool>> DeleteCommentAsync(DeleteCommentRequest dto);
        Task<Result<TaskResponse>> EditCommentAsync(UpdateCommentRequest dto);


    }
}
