using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;

namespace TaskManagement.Infrastructure.Data.Json.Repositories;

public class TaskRepositoryJson : ITaskRepository
{
    private readonly FileStorage _Storage;

    public TaskRepositoryJson(string filePath)
    {
        _Storage = new FileStorage(filePath);
    }

    public async Task AddAsync(Domain.Tasks.Task task)
    {
        var tasks = await _Storage.LoadAsync<Domain.Tasks.Task>();
        tasks.Add(task);
        await _Storage.SaveAsync(tasks);
    }

    public async Task<bool> DeleteAsync(TaskId taskId)
    {
        var tasks = await _Storage.LoadAsync<Domain.Tasks.Task>();
        var task = tasks.FirstOrDefault(u => u.Id == taskId);

        if (task is not null)
        {
            tasks.Remove(task);
            await _Storage.SaveAsync(tasks);
            return true;
        }

        return false;
    }

    public async Task<Domain.Tasks.Task?> GetByIdAsync(TaskId taskId)
    {
        var tasks = await _Storage.LoadAsync<Domain.Tasks.Task>();
        return tasks.FirstOrDefault(u => u.Id == taskId);
    }

    public async Task<bool> IsProjectTaskExistByTitle(ProjectId projectId, Title title)
    {
        var tasks = await _Storage.LoadAsync<Domain.Tasks.Task>();
        return tasks.Any(u => u.ProjectId == projectId && u.Title == title);
    }

    public async Task<List<Domain.Tasks.Task>> ListTasksByProjectIdAsync(ProjectId projectId)
    {
        var tasks = await _Storage.LoadAsync<Domain.Tasks.Task>();
        return tasks.Where(u => u.ProjectId == projectId).ToList();
    }

    public async Task UpdateAsync(Domain.Tasks.Task task)
    {
        var tasks = await _Storage.LoadAsync<Domain.Tasks.Task>();

        var index = tasks.FindIndex(u => u.Id == task.Id);
        if (index == -1)
            throw new InvalidOperationException($"task {task.Id} not found.");
        tasks[index] = task;
        await _Storage.SaveAsync(tasks);
    }
}