using AutoMapper;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Domain.Projects;

using TaskManagement.Infrastructure.Data.Json.FileHandling;
using TaskManagement.Infrastructure.DTOs;

namespace TaskManagement.Infrastructure.Data.Json.Repositories;

public class ProjectRepositoryJson(ProjectFilePath projectFile,IMapper mapper) : IProjectRepository
{
    private readonly FileStorage _storage = new(projectFile.Path);

    private async  Task<List<ProjectDto>> Projects()
    {
        return await _storage.LoadAsync<ProjectDto>();
    }
    public async Task AddAsync(Project project)
    {
        var projects = await Projects();
        var projectDto = mapper.Map<Project, ProjectDto>(project);
        projects.Add(projectDto);
        await _storage.SaveAsync(projects);
    }

    public async Task<bool> DeleteAsync(Guid projectId)
    {
        var projects =  await Projects();
        var project = projects.FirstOrDefault(u => u.Id == projectId);
        if (project is not null)
        {
            projects.Remove(project);
            await _storage.SaveAsync(projects);
            return true;
        }

        return false;
    }

    public async Task<Project?> GetByIdAsync(Guid projectId)
    {
        var projects =  await Projects();
        var projectDto= projects.FirstOrDefault(u => u.Id == projectId);
        if (projectDto is  null) return null;
        return mapper.Map<ProjectDto, Project>(projectDto);
    }

    public async Task<bool> IsProjectExistsAsync(Guid projectId)
    {
        var projects = await Projects();
        return projects.Any(u => u.Id == projectId);
    }

    public async Task<bool> IsProjectExistsByNameAsync(string name)
    {
        var projects = await Projects();
        return projects.Any(u => u.Name == name);
    }

    public async Task<List<Project>> ListAllProjectsAsync()
    {
        var projects= await Projects();
        return projects.Select(mapper.Map<ProjectDto, Project>).ToList();
    }

    public async Task<List<Project>> ListProjectsByOwnerIdAsync(Guid ownerId)
    {
        var projects =  await Projects();
        return projects.Where(p => p.OwnerId == ownerId)
            .Select(mapper.Map<ProjectDto, Project>).ToList();
    }

    public async Task UpdateAsync(Project project)
    {
        var projects =  await Projects();

        var index = projects.FindIndex(u => u.Id == project.Id.Value);
        if (index == -1)
            throw new InvalidOperationException($"Project {project.Id} not found.");
        projects[index] = mapper.Map<Project, ProjectDto>(project);
        await _storage.SaveAsync(projects);
    }
}