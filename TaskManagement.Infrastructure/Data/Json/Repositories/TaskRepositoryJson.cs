using AutoMapper;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Infrastructure.Data.Json.FileHandling;
using TaskManagement.Infrastructure.DTOs;

namespace TaskManagement.Infrastructure.Data.Json.Repositories;

public class TaskRepositoryJson(TaskFilePath taskFile , IMapper mapper) : ITaskRepository
{
    private readonly FileStorage _storage = new(taskFile.Path);

    private async  Task<List<TaskDto>> Tasks()
    {
        return await _storage.LoadAsync<TaskDto>();
    }
    public async Task AddAsync(Domain.Tasks.Task task)
    {
        var tasks = await Tasks();
        var taskDto = mapper.Map<Domain.Tasks.Task, TaskDto>(task);
        tasks.Add(taskDto);
        await _storage.SaveAsync(tasks);
    }

    public async Task<bool> DeleteAsync(Guid taskId)
    {
        var tasks = await Tasks();
        var taskDto = tasks.FirstOrDefault(u => u.Id == taskId);

        if (taskDto is null) return false;
        
        tasks.Remove(taskDto);
        await _storage.SaveAsync(tasks);
        return true;

    }

    public async Task<Domain.Tasks.Task?> GetByIdAsync(Guid taskId)
    {
        var tasks = await Tasks();
        
         var taskDto = tasks.FirstOrDefault(u => u.Id == taskId);
         if (taskDto is null) return null;
         return mapper.Map<TaskDto,Domain.Tasks.Task>(taskDto);
    }

    public async Task<bool> IsProjectTaskExistByTitle(Guid projectId, string title)
    {
        var tasks =  await Tasks();
        return tasks.Any(u => u.ProjectId == projectId && u.Title == title);
    }

    public async Task<List<Domain.Tasks.Task>> ListTasksByProjectIdAsync(Guid projectId)
    {
        var tasks =  await Tasks();
        return tasks.Where(u => u.ProjectId == projectId)
            .Select(mapper.Map<TaskDto, Domain.Tasks.Task>).ToList();
    }

    public async Task UpdateAsync(Domain.Tasks.Task task)
    {
        var tasks =  await Tasks();
        
        var index = tasks.FindIndex(u => u.Id == task.Id.Value);
        
        if (index == -1)
            throw new InvalidOperationException($"task {task.Id} not found.");
        tasks[index] = mapper.Map<Domain.Tasks.Task, TaskDto>(task);
        await _storage.SaveAsync(tasks);
    }
}