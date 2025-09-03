
using TaskManagement.Application.Common;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Tasks.Contracts;
using TaskManagement.Application.Tasks.Mapper;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;

namespace TaskManagement.Application.Tasks.UserCase
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IClock _clock;

        public TaskService(ITaskRepository taskRepository, IClock clock)
        {
            _taskRepository = taskRepository;
            _clock = clock;
        }

        public async Task<Result<TaskResponse>> CompleteTask(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.Complete(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());

        }

        public async Task<Result<TaskResponse>> CreateTaskAsync(CreateTaskRequest dto)
        {
            var title = Title.Create(dto.Title);
            

            if (await _taskRepository.IsProjectTaskExistByTitle(dto.ProjectId, title))
                return Result<TaskResponse>.Failure(Domain.Common.Errors.DomainErrors.Task.DuplicateTitle);

            var newTask = Domain.Tasks.Task.Create(
                Title.Create(dto.Title),
                Description.Create(dto.Description),
                dto.ProjectId,
                dto.CreatedById,
                dto.AssignedToId,
                TaskPriority.FromEnum(dto.Priority),
                dto.DueDate,
                _clock);

            await _taskRepository.AddAsync(newTask);

            return Result<TaskResponse>.Success(newTask.ToDto());
        }

        public async Task<Result<bool>> DeleteTaskAsync(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<bool>.Failure(DomainErrors.Task.NotFound);
            var result = await _taskRepository.DeleteAsync(taskId);
            return result
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(DomainErrors.Task.NotFound);
        }

        public async Task<Result<TaskResponse>> GetTaskByIdAsync(TaskId taskId)
        {
           var task = await _taskRepository.GetByIdAsync(taskId);
            return task is null
                ? Result<TaskResponse>.Failure(DomainErrors.Task.NotFound)
                : Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<List<TaskResponse>>> GetTasksByProjectIdAsync(ProjectId projectId)
        {
            var tasks = await _taskRepository.ListTasksByProjectIdAsync(projectId);

            return tasks is null || !tasks.Any()
                ? Result<List<TaskResponse>>.Failure(DomainErrors.Task.NotFound)
                : Result<List<TaskResponse>>.Success(tasks.Select(t => t.ToDto()).ToList());
        }

        public async Task<Result<TaskResponse>> ReopenTask(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.ReOpen(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> SetHighPriority(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.HighPriority(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> SetLowProirity(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.LowPriority(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> SetMediumPriority(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.MediumPriority(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> StartTask(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.Start(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> UpdateDueDate(TaskId taskId, DateTime dueDate)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.UpdateDueDate(dueDate, _clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<TaskResponse>> UpdateTaskAsync(UpdateTaskRequest dto)
        {
            var task = await _taskRepository.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);

            task.UpdateDetails(
                Title.Create(dto.newTitle),
                Description.Create(dto.newDescription),
                dto.updatedById,
                _clock
                );
            await _taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());
        }
    }
}
