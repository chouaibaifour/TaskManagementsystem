using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Projects.Dtos;
using TaskManagement.Application.Projects.Dtos.Member;
using TaskManagement.Domain.Projects;

namespace TaskManagement.Application.Projects.Mapper
{
    public static class ProjectMapper
    {
        public static ProjectDto ToDto(this Project project)
        {
            return new ProjectDto(
                project.Id,
                project.Name.ToString(),
                project.Description.ToString(),
                project.OwnerId,
                project.Status.ToString(),
                project.Members.Select(
                    m => new MemberDto(
                        m.UserId,
                        m.Role.ToString(),
                        m.IsActive ? "Active" : "InActive"
                        )).ToList()
            );
        }
    }
}
