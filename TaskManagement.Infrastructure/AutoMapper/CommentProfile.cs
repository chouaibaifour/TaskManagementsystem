using AutoMapper;
using TaskManagement.Domain.Tasks;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;
using TaskManagement.Domain.Users.ValueObjects;
using TaskManagement.Infrastructure.DTOs;

namespace TaskManagement.Infrastructure.AutoMapper;

public class CommentProfile:Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ConstructUsing(c => new CommentDto(
                c.CommentId.Value,
                c.AuthorId.Value,
                c.Content.ToString(),
                c.CreatedAtUtc,
                c.UpdatedAtUtc
                
            ));
        CreateMap<CommentDto, Comment>()
            .ConstructUsing(dto => Comment.Rehydrate(
                    new CommentId(dto.Id),
                    new UserId(dto.AuthorId),
                    CommentContent.Create(dto.Content),
                    dto.CreatedAtUtc,
                    dto.UpdatedAtUtc
            ));
    }
    
}