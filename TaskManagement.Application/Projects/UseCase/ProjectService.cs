using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Application.Projects.Dtos;
using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Application.Projects.Mapper;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Projects;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;
using static TaskManagement.Domain.Common.Errors.DomainErrors;

namespace TaskManagement.Application.Projects.UseCase
{
    public class ProjectService : IProjectSerivce
    {
        private readonly IProjectRepository _repo;

        private readonly IClock _clock;

        public ProjectService(IProjectRepository projectRepository, IClock clock)
        {
            _repo = projectRepository;
            _clock = clock;
        }

        public async Task<Result<ProjectDto>> ArchiveProject(ProjectId projectId)
        {
            var project = await _repo.GetByIdAsync(projectId);
            if (project == null)
                return Result<ProjectDto>.Failure(DomainErrors.Project.NotFound);

            project.Archive(_clock);

            await _repo.UpdateAsync(project);

            return Result<ProjectDto>.Success(project.ToDto());

        }

        public async Task<Result<ProjectDto>> CompleteProject(ProjectId projectId)
        {
            var project = await _repo.GetByIdAsync(projectId);
            if (project == null)
                return Result<ProjectDto>.Failure(DomainErrors.Project.NotFound);

            project.Archive(_clock);

            await _repo.UpdateAsync(project);

            return Result<ProjectDto>.Success(project.ToDto());
        }

        public async Task<Result<ProjectDto>> CreateProjectAsync(CreateProjectDto dto)
        {
            var projectName = ProjectName.Create(dto.Name);

            if (await _repo.IsProjectExistsByNameAsync(projectName))
                return Result<ProjectDto>.Failure(DomainErrors.Project.ProjectAlreadyExistsWithThisName);

            var description = Description.Create(dto.Description);

            var project = Domain.Projects.Project.Create(
                projectName,
                description,
                dto.OwnerId,
                _clock
            );

            await _repo.AddAsync(project);
            return Result<ProjectDto>.Success(project.ToDto());

        }

        public async Task<Result<bool>> DeleteProjectAsync(ProjectId projectId)
        {
           return await _repo.DeleteProjectAsync(projectId) ?

                Result<bool>.Success(true)
                :
                Result<bool>.Failure(DomainErrors.Project.NotFound);
        }

        public async Task<Result<List<ProjectDto>>> GetAllProjectsAsync()
        {
            var projects = await _repo.ListAllProjectsAsync();

            return (projects == null || projects.Count == 0) ?
                Result<List<ProjectDto>>.Failure(DomainErrors.Project.NotFound)
                :
                Result<List<ProjectDto>>.Success(projects.Select(p => p.ToDto()).ToList());
        }

        public async Task<Result<ProjectDto>> GetProjectByIdAsync(ProjectId projectId)
        {
            var project = await _repo.GetByIdAsync(projectId);

                return (project==null)?
                    Result<ProjectDto>.Failure(DomainErrors.Project.NotFound)
                    :
                    Result<ProjectDto>.Success(project.ToDto());

        }

        public async Task<Result<List<MemberDto>>> GetProjectMembers(ProjectId projectId)
        {
            var project = await _repo.GetByIdAsync(projectId);

            if (project == null)
                return Result<List<MemberDto>>.Failure(DomainErrors.Project.NotFound);

            var members = project.Members.Select(m => m.ToDto()).ToList();

            return Result<List<MemberDto>>.Success(members);
        }

        public async Task<Result<List<ProjectDto>>> GetProjectsByOwnerIdAsync(UserId ownerId)
        {
            var projects = await _repo.ListProjectsByOwnerIdAsync(ownerId);

            return Result<List<ProjectDto>>.Success(projects.Select(p => p.ToDto()).ToList());
        }

        public async Task<Result<ProjectDto>> RestoreProject(ProjectId projectId)
        {
            var project = await _repo.GetByIdAsync(projectId);
            if (project == null)
                return Result<ProjectDto>.Failure(DomainErrors.Project.NotFound);

            project.Restore(_clock);

            await _repo.UpdateAsync(project);

            return Result<ProjectDto>.Success(project.ToDto());
        }

        public async Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectDto dto)
        {
            var project = await _repo.GetByIdAsync(dto.Id);
            if (project == null)
                return Result<ProjectDto>.Failure(DomainErrors.Project.NotFound);

            project.UpdateDetails(
                ProjectName.Create(dto.Name),
                Description.Create(dto.Description),
                _clock
            );

            await _repo.UpdateAsync(project);

            return Result<ProjectDto>.Success(project.ToDto());
        }
    }
}
