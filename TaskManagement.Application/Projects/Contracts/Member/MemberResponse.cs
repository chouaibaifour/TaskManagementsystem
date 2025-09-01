using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Dtos.Member
{
    public record struct MemberResponse(
        UserId UserId,
        string Role,
        string IsActive
        );


}
