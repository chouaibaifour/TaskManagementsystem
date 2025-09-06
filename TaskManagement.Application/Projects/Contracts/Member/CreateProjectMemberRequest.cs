using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Contracts.Member
{
    public record struct CreateProjectMemberRequest(Guid ProjectId, Guid UserId,Enum MemberRole);
}
