using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Tasks.Dtos.Comment
{
    public record struct CommentDto
        (
            CommentId Id,
            string Content,
            UserId CreatedById,
            DateTime CreatedAt
        );
    
}
