using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Dtos;
using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Abstractions
{
    public interface IProjectSerivce
    {
        Task<Result<ProjectDto>> CreateProjectAsync(CreateProjectDto dto);
        Task<Result<ProjectDto>> GetProjectByIdAsync(ProjectId projectId);
        Task<Result<List<ProjectDto>>> GetProjectsByOwnerIdAsync(UserId ownerId);
        Task<Result<bool>> DeleteProjectAsync(ProjectId projectId);
        Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectDto dto);
        Task<Result<List<ProjectDto>>> GetAllProjectsAsync();
        Task<Result<ProjectDto>> ArchiveProject(ProjectId projectId);
        Task<Result<ProjectDto>> RestoreProject(ProjectId projectId);
        Task<Result<ProjectDto>> CompleteProject(ProjectId project);
        Task<Result<List<MemberDto>>> GetProjectMembers(ProjectId projectId);
        

    }
}
