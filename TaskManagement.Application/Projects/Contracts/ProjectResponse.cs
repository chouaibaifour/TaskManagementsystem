using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Contracts
{
    public record struct ProjectResponse(
        ProjectId Id,
        string Name,
        string Description,
        UserId OwnerId,
        string Status,
        List<MemberResponse> Members
    );

}
