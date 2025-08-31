using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Tasks.Dtos.Comment;
using TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto ToDto(this Comment comment)
        {
           return new CommentDto(
                comment.CommentId,
                comment.Content.Display,
                comment.AuthorId,
                comment.CreatedAtUtc
                );
        }
    }
}
