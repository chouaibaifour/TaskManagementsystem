using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Dtos;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Abstractions
{
    public interface IProjectMemberService
    {
        Task<Result<ProjectDto>> AddProjectMember(CreateProjectMemberDto dto);
        Task<Result<ProjectDto>> RemoveProjectMember(ProjectId ProjectId, UserId UserId);
        Task<Result<ProjectDto>> DeactivateProjectMember(ProjectId ProjectId, UserId UserId);
        Task<Result<ProjectDto>> activateProjectMember(ProjectId ProjectId, UserId UserId);
    }
}
