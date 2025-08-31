using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Tasks.Dtos;
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

        public async Task<Result<TaskDto>> CompleteTask(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);
            task.Complete(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskDto>.Success(task.ToDto());

        }

        public async Task<Result<TaskDto>> CreateTaskAsync(CreateTaskDto dto)
        {
            var task = await _taskRepository.IsProjectTaskExistByTitle(dto.projectId, dto.title);
            if (task is not null)
                return Result<TaskDto>.Failure(Domain.Common.Errors.DomainErrors.Task.DuplicateTitle);

            var newTask = Domain.Tasks.Task.Create(
                Title.Create(dto.title),
                Description.Create(dto.description),
                dto.projectId,
                dto.createdById,
                dto.assignedToId,
                Priority.From(dto.priority),
                dto.dueDate,
                _clock);

            await _taskRepository.AddAsync(newTask);

            return Result<TaskDto>.Success(newTask.ToDto());
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

        public async Task<Result<TaskDto>> GetTaskByIdAsync(TaskId taskId)
        {
           var task = await _taskRepository.GetByIdAsync(taskId);
            return task is null
                ? Result<TaskDto>.Failure(DomainErrors.Task.NotFound)
                : Result<TaskDto>.Success(task.ToDto());
        }

        public async Task<Result<List<TaskDto>>> GetTasksByProjectIdAsync(ProjectId projectId)
        {
            var tasks = await _taskRepository.ListTasksByProjectIdAsync(projectId);

            return tasks is null || !tasks.Any()
                ? Result<List<TaskDto>>.Failure(DomainErrors.Task.NotFound)
                : Result<List<TaskDto>>.Success(tasks.Select(t => t.ToDto()).ToList());
        }

        public async Task<Result<TaskDto>> ReopenTask(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);
            task.ReOpen(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskDto>.Success(task.ToDto());
        }

        public async Task<Result<TaskDto>> SetHighPriority(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);
            task.HighPriority(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskDto>.Success(task.ToDto());
        }

        public async Task<Result<TaskDto>> SetLowProirity(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);
            task.LowPriority(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskDto>.Success(task.ToDto());
        }

        public async Task<Result<TaskDto>> SetMediumPriority(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);
            task.MediumPriority(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskDto>.Success(task.ToDto());
        }

        public async Task<Result<TaskDto>> StartTask(TaskId taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);
            task.Start(_clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskDto>.Success(task.ToDto());
        }

        public async Task<Result<TaskDto>> UpdateDueDate(TaskId taskId, DateTime dueDate)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);
            task.UpdateDueDate(dueDate, _clock);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskDto>.Success(task.ToDto());
        }

        public async Task<Result<TaskDto>> UpdateTaskAsync(UpdateTaskDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);

            task.UpdateDetails(
                Title.Create(dto.newTitle),
                Description.Create(dto.newDescription),
                dto.updatedById,
                _clock
                );
            await _taskRepository.UpdateAsync(task);
            return Result<TaskDto>.Success(task.ToDto());
        }
    }
}
