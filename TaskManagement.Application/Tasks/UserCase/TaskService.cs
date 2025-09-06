
using TaskManagement.Application.Common;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Tasks.Contracts;
using TaskManagement.Application.Tasks.Contracts.Comment;
using TaskManagement.Application.Tasks.Mapper;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;

namespace TaskManagement.Application.Tasks.UserCase
{
    public class TaskService(ITaskRepository taskRepository, IClock clock) : ITaskService
    {
        public async Task<Result<TaskResponse>> CompleteTask(TaskId taskId)
        {
            var task = await taskRepository.GetByIdAsync(taskId.Value);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.Complete(clock);
            await taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());

        }

        public async Task<Result<TaskResponse>> CreateTaskAsync(CreateTaskRequest dto)
        {
            
            

            if (await taskRepository.IsProjectTaskExistByTitle(dto.ProjectId, dto.Title))
                return Result<TaskResponse>.Failure(DomainErrors.Task.DuplicateTitle);

            var newTask = Domain.Tasks.Task.Create(
                Title.Create(dto.Title),
                Description.Create(dto.Description),
                dto.ProjectId,
                dto.CreatedById,
                dto.AssignedToId,
                TaskPriority.FromEnum(dto.Priority),
                dto.DueDate,
                clock);

            await taskRepository.AddAsync(newTask);

            return Result<TaskResponse>.Success(newTask.ToDto());
        }

        public async Task<Result<bool>> DeleteTaskAsync(TaskId taskId)
        {
            var task = await taskRepository.GetByIdAsync(taskId.Value);
            if (task is null)
                return Result<bool>.Failure(DomainErrors.Task.NotFound);
            var result = await taskRepository.DeleteAsync(taskId);
            return result
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(DomainErrors.Task.NotFound);
        }

        public async Task<Result<TaskResponse>> GetTaskByIdAsync(TaskId taskId)
        {
           var task = await taskRepository.GetByIdAsync(taskId.Value);
            return task is null
                ? Result<TaskResponse>.Failure(DomainErrors.Task.NotFound)
                : Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<List<TaskResponse>>> GetTasksByProjectIdAsync(ProjectId projectId)
        {
            var tasks = await taskRepository.ListTasksByProjectIdAsync(projectId);

            return  tasks.Count == 0
                ? Result<List<TaskResponse>>.Failure(DomainErrors.Task.NotFound)
                : Result<List<TaskResponse>>.Success(tasks.Select(t => t.ToDto()).ToList());
        }

        public async Task<Result<TaskResponse>> ReopenTask(TaskId taskId)
        {
            var task = await taskRepository.GetByIdAsync(taskId.Value);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.ReOpen(clock);
            await taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> SetHighPriority(TaskId taskId)
        {
            var task = await taskRepository.GetByIdAsync(taskId.Value);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.HighPriority(clock);
            await taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> SetLowPriority(TaskId taskId)
        {
            var task = await taskRepository.GetByIdAsync(taskId.Value);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.LowPriority(clock);
            await taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> SetMediumPriority(TaskId taskId)
        {
            var task = await taskRepository.GetByIdAsync(taskId.Value);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.MediumPriority(clock);
            await taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> StartTask(TaskId taskId)
        {
            var task = await taskRepository.GetByIdAsync(taskId.Value);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.Start(clock);
            await taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> UpdateDueDate(TaskId taskId, DateTime dueDate)
        {
            var task = await taskRepository.GetByIdAsync(taskId.Value);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.UpdateDueDate(dueDate, clock);
            await taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> UpdateTaskAsync(UpdateTaskRequest dto)
        {
            var task = await taskRepository.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);

            task.UpdateDetails(
                Title.Create(dto.NewTitle),
                Description.Create(dto.NewDescription),
                dto.UpdatedById,
                clock
                );
            await taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }
        
        public async Task<Result<TaskResponse>> AddCommentAsync(CreateCommentRequest dto)
        {
            var task = await taskRepository.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.AddComment(
                CommentId.New(),
                dto.UserId, 
                CommentContent.Create(dto.CommentContent), 
                clock
            );

            await taskRepository.UpdateAsync(task);

            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<bool>> DeleteCommentAsync(DeleteCommentRequest dto)
        {
            var task = await taskRepository.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<bool>.Failure(DomainErrors.Task.NotFound);

            task.DeleteComment(dto.CommentId, dto.DeletedById,clock);

            await taskRepository.UpdateAsync(task);

            return Result<bool>.Success(true);
        }

        public async Task<Result<TaskResponse>> EditCommentAsync(UpdateCommentRequest dto)
        {

            var task = await taskRepository.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);

            task.EditComment(dto.CommentId,CommentContent.Create(dto.NewContent), dto.UserId,  clock);

            await taskRepository.UpdateAsync(task);

            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<IEnumerable<CommentResponse>>> GetCommentsByTaskIdAsync(TaskId taskId)
        {
            var task = await taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<IEnumerable<CommentResponse>>.Failure(DomainErrors.Task.NotFound);
            var comments = task.Comments.Select(c => c.ToResponse());
            return Result<IEnumerable<CommentResponse>>.Success(comments);

        }
    }
}
