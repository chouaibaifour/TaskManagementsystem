using AutoMapper;
using TaskManagement.Domain.Common.Primitives.ValueObject;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;
using TaskManagement.Infrastructure.DTOs;
using Task = TaskManagement.Domain.Tasks.Task;
using TaskStatus = TaskManagement.Domain.Tasks.ValueObjects.TaskStatus;

namespace TaskManagement.Infrastructure.AutoMapper;

public class TaskProfile:Profile
{
  

    public TaskProfile(IMapper mapper)
    {
        
        CreateMap<Domain.Tasks.Task, TaskDto>()
            .ConstructUsing(t => new TaskDto
            (
                t.Id.Value,
                t.Title.ToString(),
                t.Description.ToString(),
                t.TaskStatus.ToEnum(),
                t.TaskPriority.ToEnum(),
                t.DueDate,
                t.CreatedById.Value,
                t.AssignedToId.Value,
                t.ProjectId.Value,
                t.CreatedAtUtc,
                t.UpdatedAtUtc, 
                t.Comments.Select(mapper.Map<Comment,CommentDto>).ToList()
            ));

        CreateMap<TaskDto, Domain.Tasks.Task>()
            .ConstructUsing(dto => Task.Rehydrate(
                new TaskId(dto.Id),
                Title.Create(dto.Title),
                Description.Create(dto.Description),
                TaskStatus.FromEnum(dto.Status),
                TaskPriority.FromEnum(dto.Priority),
                dto.DueDate,
                new UserId(dto.CreatedById),
                new UserId(dto.AssignedToId),
                new ProjectId(dto.ProjectId),
                dto.CreatedAtUtc,
                dto.UpdateAtUtc,
                dto.Comments.Select(mapper.Map<CommentDto, Comment>).ToList()



            ));
    }
}