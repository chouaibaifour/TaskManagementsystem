using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Application.Projects.Contracts;
using TaskManagement.Application.Projects.Contracts.Member;
using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Application.Projects.Mapper;
using TaskManagement.Application.Users.interfaces;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.UseCase
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IProjectRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IClock _clock;
        public ProjectMemberService(IProjectRepository repo, IUserRepository userRepo, IClock clock)
        {
            _repo = repo;
            _clock = clock;
            _userRepo = userRepo;
        }

        public async Task<Result<ProjectResponse>> activateProjectMember(ProjectId ProjectId, UserId UserId)
        {
            var project = await _repo.GetByIdAsync(ProjectId);
            if (project is null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            if (await _userRepo.GetByIdAsync(UserId) is null)
                return Result<ProjectResponse>.Failure(DomainErrors.User.NotFound);

            project.ActivateMember(UserId, _clock);

            await _repo.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());
        }

        public async Task<Result<ProjectResponse>> AddProjectMember(CreateProjectMemberRequest dto)
        {
            var project = await _repo.GetByIdAsync(dto.ProjectId);
            if (project is null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            if(await _userRepo.GetByIdAsync(dto.UserId) is null)
                return Result<ProjectResponse>.Failure(DomainErrors.User.NotFound);

            project.AddMember(dto.UserId, _clock);

            await _repo.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());


        }

        public async Task<Result<ProjectResponse>> DeactivateProjectMember(ProjectId ProjectId, UserId UserId)
        {
            var project = await _repo.GetByIdAsync(ProjectId);
            if (project is null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            if (await _userRepo.GetByIdAsync(UserId) is null)
                return Result<ProjectResponse>.Failure(DomainErrors.User.NotFound);

            project.DeactivateMember(UserId, _clock);

            await _repo.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());
        }

        public async Task<Result<ProjectResponse>> RemoveProjectMember(ProjectId ProjectId, UserId UserId)
        {
            var project = await _repo.GetByIdAsync(ProjectId);
            if (project is null)
                return Result<ProjectResponse>.Failure(DomainErrors.Project.NotFound);

            if (await _userRepo.GetByIdAsync(UserId) is null)
                return Result<ProjectResponse>.Failure(DomainErrors.User.NotFound);

            project.RemoveMember(UserId, _clock);

            await _repo.UpdateAsync(project);

            return Result<ProjectResponse>.Success(project.ToDto());
        }
    }
}
