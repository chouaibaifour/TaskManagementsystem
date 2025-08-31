using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Dtos
{
    public record struct ProjectDto(
        ProjectId Id,
        string Name,
        string Description,
        UserId OwnerId,
        string Status,
        List<MemberDto> Members
    );

}
