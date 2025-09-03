using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Domain.Projects;

namespace TaskManagement.Application.Projects.Mapper
{
     public static class ProjectMemberMapper
    {
        public static MemberResponse ToDto(this Member projectMember)
        {
            return new MemberResponse(
                projectMember.UserId,
                projectMember.Role.Display,
                projectMember.IsActive ? "Active" : "InActive"
                );
        }
    }
}
