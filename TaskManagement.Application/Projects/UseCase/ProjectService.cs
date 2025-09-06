
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Application.Projects.Contracts;
using TaskManagement.Application.Projects.Contracts.Member;
using TaskManagement.Application.Projects.Mapper;
using TaskManagement.Application.Users.Abstractions;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Projects.ValueObjects;


namespace TaskManagement.Application.Projects.UseCase
{
    public class ProjectService(IProjectRepository projectRepository,
        IUserRepository userRepository,IClock clock) : IProjectService
    {
        public async Task<Result<ProjectResponse>> ArchiveProject(Guid projectId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);
            if (project == null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            project.Archive(clock);

            await projectRepository.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());

        }

        public async Task<Result<ProjectResponse>> CompleteProject(Guid projectId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);
            if (project == null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            project.Archive(clock);

            await projectRepository.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());
        }

        public async Task<Result<ProjectResponse>> CreateProjectAsync(CreateProjectRequest dto)
        {
            
            if (await projectRepository.IsProjectExistsByNameAsync(dto.Name))
                return Result<ProjectResponse>.Failure(DomainErrors.Project.ProjectAlreadyExistsWithThisName);

            

            var project = Domain.Projects.Project.Create(
                ProjectName.Create(dto.Name),
                Description.Create(dto.Description),
                dto.OwnerId,
                clock
            );

            await projectRepository.AddAsync(project);
            return Result<ProjectResponse>.Success(project.ToDto());

        }

        public async Task<Result<bool>> DeleteProjectAsync(Guid projectId)
        {
           return await projectRepository.DeleteAsync(projectId) ?

                Result<bool>.Success(true)
                :
                Result<bool>.Failure(DomainErrors.Project.NotFound);
        }

        public async Task<Result<List<ProjectResponse>>> GetAllProjectsAsync()
        {
            var projects = await projectRepository.ListAllProjectsAsync();

            return (projects.Count == 0) ?
                Result<List<ProjectResponse>>.Failure(DomainErrors.Project.NotFound)
                :
                Result<List<ProjectResponse>>.Success(projects.Select(p => p.ToDto()).ToList());
        }

        public async Task<Result<ProjectResponse>> GetProjectByIdAsync(Guid projectId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);

                return (project==null)?
                    Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound)
                    :
                    Result<ProjectResponse>.Success(project.ToDto());

        }

        public async Task<Result<List<MemberResponse>>> GetProjectMembers(Guid projectId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);

            if (project == null)
                return Result<List<MemberResponse>>.Failure(DomainErrors.Project.NotFound);

            var members = project.Members.Select(m => m.ToDto()).ToList();

            return Result<List<MemberResponse>>.Success(members);
        }

        public async Task<Result<List<ProjectResponse>>> GetProjectsByOwnerIdAsync(Guid ownerId)
        {
            var projects = await projectRepository.ListProjectsByOwnerIdAsync(ownerId);

            return Result<List<ProjectResponse>>.Success(projects.Select(p => p.ToDto()).ToList());
        }

        public async Task<Result<ProjectResponse>> RestoreProject(Guid projectId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);
            if (project == null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            project.Restore(clock);

            await projectRepository.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());
        }

        public async Task<Result<ProjectResponse>> UpdateProjectAsync(UpdateProjectRequest dto)
        {
            var project = await projectRepository.GetByIdAsync(dto.Id);
            if (project == null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            project.UpdateDetails(
                ProjectName.Create(dto.Name),
                Description.Create(dto.Description),
                clock
            );

            await projectRepository.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());
        }
        
        public async Task<Result<ProjectResponse>> ActivateProjectMember(Guid projectId, Guid userId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);
            if (project is null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            if (await userRepository.GetByIdAsync(userId) is null)
                return Result<ProjectResponse>.Failure(DomainErrors.User.NotFound);

            project.ActivateMember(userId, clock);

            await projectRepository.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());
        }

        public async Task<Result<ProjectResponse>> AddProjectMember(CreateProjectMemberRequest dto)
        {
            var project = await projectRepository.GetByIdAsync(dto.ProjectId);
            if (project is null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            if(await userRepository.GetByIdAsync(dto.UserId) is null)
                return Result<ProjectResponse>.Failure(DomainErrors.User.NotFound);

            project.AddMember(dto.UserId, clock);

            await projectRepository.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());


        }

        public async Task<Result<ProjectResponse>> DeactivateProjectMember(Guid projectId, Guid userId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);
            if (project is null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            if (await userRepository.GetByIdAsync(userId) is null)
                return Result<ProjectResponse>.Failure(DomainErrors.User.NotFound);

            project.DeactivateMember(userId, clock);

            await projectRepository.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());
        }

        public async Task<Result<ProjectResponse>> RemoveProjectMember(Guid projectId, Guid userId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);
            if (project is null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            if (await userRepository.GetByIdAsync(userId) is null)
                return Result<ProjectResponse>.Failure(DomainErrors.User.NotFound);

            project.RemoveMember(userId, clock);

            await projectRepository.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());
        }
    }
}
