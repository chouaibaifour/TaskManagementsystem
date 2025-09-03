using AutoMapper;
using TaskManagement.Domain.Projects;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;
using TaskManagement.Infrastructure.DTOs;

namespace TaskManagement.Infrastructure.AutoMapper;

public class ProjectMemberProfile : Profile
{
    public ProjectMemberProfile()
    {
        CreateMap<Member, ProjectMemberDto>()
            .ConstructUsing( m=>new ProjectMemberDto(
                    m.UserId.Value,
                    m.Role.ToEnum(),
                    m.JoinedAtUtc,
                    m.IsActive,
                    m.AssignedTaskIds.Select(t=>t.Value).ToList()
                    )  
                
            );
        CreateMap<ProjectMemberDto, Member>()
            .ConstructUsing(dto => Member.Rehydrate(
                new UserId(dto.UserId),
                MemberRole.FromEnum(dto.MemberRole),
                dto.JoinedAtUtc,
                dto.IsActive,
                dto.AssignedTaskIds.Select(t=>new TaskId(t)).ToList()
            ));
    }
    
}