
using TaskManagement.Application.Projects.Contracts;
using TaskManagement.Application.Projects.Contracts.Member;
using TaskManagement.Domain.Projects;

namespace TaskManagement.Application.Projects.Mapper
{
    public static class ProjectMapper
    {
        public static ProjectResponse ToDto(this Project project)
        {
            return new ProjectResponse(
                project.Id,
                project.Name.ToString(),
                project.Description.ToString(),
                project.OwnerId,
                project.Status.ToString(),
                project.Members.Select(
                    m => new MemberResponse(
                        m.UserId,
                        m.Role.ToString(),
                        m.IsActive ? "Active" : "InActive"
                        )).ToList()
            );
        }
    }
}
