using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Tasks.Contracts.Comment;

using TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Mapper
{
    public static class CommentMapper
    {
        public static CommentResponse ToResponse(this Comment comment)
        {
           return new CommentResponse(
                comment.CommentId,
                comment.Content.Display,
                comment.AuthorId,
                comment.CreatedAtUtc
                );
        }
    }
}
