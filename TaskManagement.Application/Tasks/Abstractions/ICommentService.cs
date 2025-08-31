using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Tasks.Dtos;
using TaskManagement.Application.Tasks.Dtos.Comment;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Tasks.Abstractions
{
    public interface ICommentService
    {
        Task<Result<TaskDto>> AddCommentAsync(CreateCommentDto dto);
        Task<Result<IEnumerable<CommentDto>>> GetCommentsByTaskIdAsync(TaskId taskId);
        Task<Result<bool>> DeleteCommentAsync(DeleteCommentDto dto);
        Task<Result<TaskDto>> EditCommentAsync(UpdateCommentDto dto);
        
    }
}
