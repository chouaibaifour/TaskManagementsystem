using AutoMapper;
using TaskManagement.Domain.Projects;
using TaskManagement.Infrastructure.DTOs;

namespace TaskManagement.Infrastructure.AutoMapper;

public class ProjectProfile:Profile
{

    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>()
            .ForMember(dst => dst.Name,
                src => src.MapFrom(dto => dto.Name))
            .ForMember(dst => dst.Description,
                src => src.MapFrom(dto => dto.Description.ToString()))
            .ForMember(dst => dst.OwnerId,
                src => src.MapFrom(dto => dto.OwnerId.ToString()))
            .ForMember(dst => dst.Status,
                src => src.MapFrom(dto => dto.Status.ToString()));

    }
}