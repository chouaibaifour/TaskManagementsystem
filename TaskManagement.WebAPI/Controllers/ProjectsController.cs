using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Application.Projects.Contracts;
using TaskManagement.Application.Projects.Contracts.Member;

namespace TaskManagement.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest dto)
    {
        var result = await projectService.CreateProjectAsync(dto);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{projectId:guid}")]
    public async Task<IActionResult> GetProjectById(Guid projectId)
    {
        var result = await projectService.GetProjectByIdAsync(projectId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("owner/{ownerId:guid}")]
    public async Task<IActionResult> GetProjectsByOwnerId(Guid ownerId)
    {
        var result = await projectService.GetProjectsByOwnerIdAsync(ownerId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("{projectId:guid}")]
    public async Task<IActionResult> DeleteProject(Guid projectId)
    {
        var result = await projectService.DeleteProjectAsync(projectId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{projectId:guid}")]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest dto)
    {
        var result = await projectService.UpdateProjectAsync(dto);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var result = await projectService.GetAllProjectsAsync();
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{projectId:guid}/archive")]
    public async Task<IActionResult> ArchiveProject(Guid projectId)
    {
        var result = await projectService.ArchiveProjectAsync(projectId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{projectId:guid}/restore")]
    public async Task<IActionResult> RestoreProject(Guid projectId)
    {
        var result = await projectService.RestoreProjectAsync(projectId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{projectId:guid}/complete")]
    public async Task<IActionResult> CompleteProject(Guid projectId)
    {
        var result = await projectService.CompleteProjectAsync(projectId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{projectId:guid}/members")]
    public async Task<IActionResult> GetProjectMembers(Guid projectId)
    {
        var result = await projectService.GetProjectMembersAsync(projectId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("members")]
    public async Task<IActionResult> AddProjectMember([FromBody] CreateUpdateProjectMemberRequest dto)
    {
        var result = await projectService.AddProjectMemberAsync(dto);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("members")]
    public async Task<IActionResult> UpdateProjectMember([FromBody] CreateUpdateProjectMemberRequest dto)
    {
        var result = await projectService.UpdateProjectMemberAsync(dto);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("{projectId:guid}/members/{userId:guid}")]
    public async Task<IActionResult> RemoveProjectMember(Guid projectId, Guid userId)
    {
        var result = await projectService.RemoveProjectMemberAsync(projectId, userId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{projectId:guid}/members/{userId:guid}/deactivate")]
    public async Task<IActionResult> DeactivateProjectMember(Guid projectId, Guid userId)
    {
        var result = await projectService.DeactivateProjectMemberAsync(projectId, userId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{projectId:guid}/members/{userId:guid}/activate")]
    public async Task<IActionResult> ActivateProjectMember(Guid projectId, Guid userId)
    {
        var result = await projectService.ActivateProjectMemberAsync(projectId, userId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

  
}