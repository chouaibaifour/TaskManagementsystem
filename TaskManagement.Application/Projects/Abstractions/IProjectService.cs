
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
        Task<Result<ProjectResponse>> ArchiveProjectAsync(Guid projectId);
        Task<Result<ProjectResponse>> RestoreProjectAsync(Guid projectId);
        Task<Result<ProjectResponse>> CompleteProjectAsync(Guid project);
        Task<Result<List<MemberResponse>>> GetProjectMembersAsync(Guid projectId);
        
        Task<Result<ProjectResponse>> AddProjectMemberAsync(CreateUpdateProjectMemberRequest dto);

        Task<Result<ProjectResponse>> UpdateProjectMemberAsync(CreateUpdateProjectMemberRequest dto);
        Task<Result<ProjectResponse>> RemoveProjectMemberAsync(Guid projectId, Guid userId);
        Task<Result<ProjectResponse>> DeactivateProjectMemberAsync(Guid projectId, Guid userId);
        Task<Result<ProjectResponse>> ActivateProjectMemberAsync(Guid projectId, Guid userId);
        
        

    }
}
