using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Tasks.Dtos;
using TaskManagement.Application.Tasks.Dtos.Comment;
using TaskManagement.Application.Tasks.Mapper;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects.Comment;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Tasks.UserCase
{
    public class CommentService : ICommentService
    {
        private readonly ITaskRepository _repo;
        private readonly IClock _clock;
        public CommentService(ITaskRepository repo, IClock clock)
        {
            _repo = repo;
            _clock = clock;
        }

        public async Task<Result<TaskDto>> AddCommentAsync(CreateCommentDto dto)
        {
            var task = await _repo.GetByIdAsync(dto.taskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);
            task.AddComment(
                        CommentId.New(),
                        dto.userId, 
                        CommentContent.Create(dto.commentContent), 
                        _clock
                     );

            await _repo.UpdateAsync(task);

            return Result<TaskDto>.Success(task.ToDto());
        }

        public async Task<Result<bool>> DeleteCommentAsync(DeleteCommentDto dto)
        {
            var task = await _repo.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<bool>.Failure(DomainErrors.Task.NotFound);

            task.DeleteComment(dto.CommentId, dto.UserId,_clock);

            await _repo.UpdateAsync(task);

            return Result<bool>.Success(true);
        }

        public async Task<Result<TaskDto>> EditCommentAsync(UpdateCommentDto dto)
        {

            var task = await _repo.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskDto>.Failure(DomainErrors.Task.NotFound);

            task.EditComment(dto.CommentId,CommentContent.Create(dto.NewContent), dto.UserId,  _clock);

            await _repo.UpdateAsync(task);

            return Result<TaskDto>.Success(task.ToDto());
        }

        public async Task<Result<IEnumerable<CommentDto>>> GetCommentsByTaskIdAsync(TaskId taskId)
        {
            var task = await _repo.GetByIdAsync(taskId);
            if (task is null)
                return Result<IEnumerable<CommentDto>>.Failure(DomainErrors.Task.NotFound);
            var comments = task.Comments.Select(c => c.ToDto());
            return Result<IEnumerable<CommentDto>>.Success(comments);

        }
    }
}
