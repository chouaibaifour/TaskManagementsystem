using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Contracts;
using TaskManagement.Application.Projects.Dtos;
using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Abstractions
{
    public interface IProjectService
    {
        Task<Result<ProjectResponse>> CreateProjectAsync(CreateProjectRequest dto);
        Task<Result<ProjectResponse>> GetProjectByIdAsync(ProjectId projectId);
        Task<Result<List<ProjectResponse>>> GetProjectsByOwnerIdAsync(UserId ownerId);
        Task<Result<bool>> DeleteProjectAsync(ProjectId projectId);
        Task<Result<ProjectResponse>> UpdateProjectAsync(UpdateProjectRequest dto);
        Task<Result<List<ProjectResponse>>> GetAllProjectsAsync();
        Task<Result<ProjectResponse>> ArchiveProject(ProjectId projectId);
        Task<Result<ProjectResponse>> RestoreProject(ProjectId projectId);
        Task<Result<ProjectResponse>> CompleteProject(ProjectId project);
        Task<Result<List<MemberResponse>>> GetProjectMembers(ProjectId projectId);
        

    }
}
