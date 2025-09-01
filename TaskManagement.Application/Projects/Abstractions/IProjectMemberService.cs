using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Contracts;
using TaskManagement.Application.Projects.Contracts.Member;
using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Abstractions
{
    public interface IProjectMemberService
    {
        Task<Result<ProjectResponse>> AddProjectMember(CreateProjectMemberRequest dto);
        Task<Result<ProjectResponse>> RemoveProjectMember(ProjectId ProjectId, UserId UserId);
        Task<Result<ProjectResponse>> DeactivateProjectMember(ProjectId ProjectId, UserId UserId);
        Task<Result<ProjectResponse>> activateProjectMember(ProjectId ProjectId, UserId UserId);
    }
}
