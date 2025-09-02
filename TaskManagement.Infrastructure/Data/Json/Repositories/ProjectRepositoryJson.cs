using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Domain.Projects;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;
using TaskManagement.Infrastructure.Data.Json.FileHandling;

namespace TaskManagement.Infrastructure.Data.Json.Repositories;

public class ProjectRepositoryJson(string filePath) : IProjectRepository
{
    private readonly FileStorage _storage = new(filePath);

    public async Task AddAsync(Project project)
    {
        var projects = await _storage.LoadAsync<Project>();
        projects.Add(project);
        await _storage.SaveAsync(projects);
    }

    public async Task<bool> DeleteAsync(ProjectId projectId)
    {
        var projects = await _storage.LoadAsync<Project>();
        var project = projects.FirstOrDefault(u => u.Id == projectId);
        if (project is not null)
        {
            projects.Remove(project);
            await _storage.SaveAsync(projects);
            return true;
        }

        return false;
    }

    public async Task<Project?> GetByIdAsync(ProjectId projectId)
    {
        var projects = await _storage.LoadAsync<Project>();
        return projects.FirstOrDefault(u => u.Id == projectId);
    }

    public async Task<bool> IsProjectExistsAsync(ProjectId projectId)
    {
        var projects = await _storage.LoadAsync<Project>();
        return projects.Any(u => u.Id == projectId);
    }

    public async Task<bool> IsProjectExistsByNameAsync(ProjectName name)
    {
        var projects = await _storage.LoadAsync<Project>();
        return projects.Any(u => u.Name == name);
    }

    public async Task<List<Project>> ListAllProjectsAsync()
    {
        return await _storage.LoadAsync<Project>();
    }

    public async Task<List<Project>> ListProjectsByOwnerIdAsync(UserId ownerId)
    {
        var projects = await _storage.LoadAsync<Project>();
        return projects.Where(p => p.OwnerId == ownerId).ToList();
    }

    public async Task UpdateAsync(Project project)
    {
        var projects = await _storage.LoadAsync<Project>();

        var index = projects.FindIndex(u => u.Id == project.Id);
        if (index == -1)
            throw new InvalidOperationException($"Project {project.Id} not found.");
        projects[index] = project;
        await _storage.SaveAsync(projects);
    }
}