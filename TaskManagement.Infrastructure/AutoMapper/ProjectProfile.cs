using AutoMapper;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Projects;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;
using TaskManagement.Infrastructure.DTOs;

namespace TaskManagement.Infrastructure.AutoMapper;

public class ProjectProfile:Profile
{

    public ProjectProfile(IMapper mapper)
    {
        CreateMap<Project, ProjectDto>()
            .ConstructUsing(p => new ProjectDto(
                p.Id.Value,
                p.Name.ToString(),
                p.Description.ToString(),
                p.OwnerId.Value,
                p.Status.ToString(),
                p.CreatedAtUtc,
                p.UpdateAtUtc,
                p.Members.Select(m=> mapper.Map<Member,ProjectMemberDto>(m)).ToList()
            ));

        CreateMap<ProjectDto, Project>()
            .ConstructUsing(dto =>  Project.Rehydrate(
                new ProjectId(dto.Id),
                ProjectName.Create(dto.Name),
                Description.Create(dto.Description),
                new UserId(dto.OwnerId),
                ProjectStatus.FromString(dto.Status),
                dto.CreatedAtUtc,
                dto.UpdatedAtUtc,
                dto.Members.Select(m
                    =>mapper.Map<ProjectMemberDto,Member>(m)).ToList()
              
                
                
                
            ));

    }
}