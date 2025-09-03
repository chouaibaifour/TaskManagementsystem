using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Common;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Tasks.Contracts;
using TaskManagement.Application.Tasks.Contracts.Comment;
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

        public async Task<Result<TaskResponse>> AddCommentAsync(CreateCommentRequest dto)
        {
            var task = await _repo.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            task.AddComment(
                        CommentId.New(),
                        dto.UserId, 
                        CommentContent.Create(dto.CommentContent), 
                        _clock
                     );

            await _repo.UpdateAsync(task);

            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<bool>> DeleteCommentAsync(DeleteCommentRequest dto)
        {
            var task = await _repo.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<bool>.Failure(DomainErrors.Task.NotFound);

            task.DeleteComment(dto.CommentId, dto.UserId,_clock);

            await _repo.UpdateAsync(task);

            return Result<bool>.Success(true);
        }

        public async Task<Result<TaskResponse>> EditCommentAsync(UpdateCommentRequest dto)
        {

            var task = await _repo.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);

            task.EditComment(dto.CommentId,CommentContent.Create(dto.NewContent), dto.UserId,  _clock);

            await _repo.UpdateAsync(task);

            return Result<TaskResponse>.Success(task.ToDto());
        }

        public async Task<Result<IEnumerable<CommentResponse>>> GetCommentsByTaskIdAsync(TaskId taskId)
        {
            var task = await _repo.GetByIdAsync(taskId);
            if (task is null)
                return Result<IEnumerable<CommentResponse>>.Failure(DomainErrors.Task.NotFound);
            var comments = task.Comments.Select(c => c.ToResponse());
            return Result<IEnumerable<CommentResponse>>.Success(comments);

        }
    }
}
