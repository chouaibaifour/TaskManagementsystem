using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Domain.Projects;

namespace TaskManagement.Application.Projects.Mapper
{
     public static class ProjectMemberDto
    {
        public static MemberDto ToDto(this Member projectMember)
        {
            return new MemberDto(
                projectMember.UserId,
                projectMember.Role.Display,
                projectMember.IsActive ? "Active" : "InActive"
                );
        }
    }
}
