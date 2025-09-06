
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Contracts;
using TaskManagement.Application.Projects.Contracts.Member;



namespace TaskManagement.Application.Projects.Abstractions
{
    public interface IProjectService
    {
        Task<Result<ProjectResponse>> CreateProjectAsync(CreateProjectRequest dto);
        Task<Result<ProjectResponse>> GetProjectByIdAsync(Guid projectId);
        Task<Result<List<ProjectResponse>>> GetProjectsByOwnerIdAsync(Guid ownerId);
        Task<Result<bool>> DeleteProjectAsync(Guid projectId);
        Task<Result<ProjectResponse>> UpdateProjectAsync(UpdateProjectRequest dto);
        Task<Result<List<ProjectResponse>>> GetAllProjectsAsync();
        Task<Result<ProjectResponse>> ArchiveProject(Guid projectId);
        Task<Result<ProjectResponse>> RestoreProject(Guid projectId);
        Task<Result<ProjectResponse>> CompleteProject(Guid project);
        Task<Result<List<MemberResponse>>> GetProjectMembers(Guid projectId);
        
        Task<Result<ProjectResponse>> AddProjectMember(CreateProjectMemberRequest dto);
        Task<Result<ProjectResponse>> RemoveProjectMember(Guid projectId, Guid userId);
        Task<Result<ProjectResponse>> DeactivateProjectMember(Guid projectId, Guid userId);
        Task<Result<ProjectResponse>> ActivateProjectMember(Guid projectId, Guid userId);
        

    }
}
