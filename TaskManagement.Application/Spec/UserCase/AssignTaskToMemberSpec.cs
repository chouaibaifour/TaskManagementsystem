
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Application.Spec.Abstractions;
using TaskManagement.Application.Spec.Contracts;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Tasks.Contracts;
using TaskManagement.Application.Tasks.Mapper;
using TaskManagement.Application.Users.Abstractions;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Services;

namespace TaskManagement.Application.Spec.UserCase
{
    public class AssignTaskToMemberSpec(
        IProjectRepository projectRepository,
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        IClock clock)
        : IAssignTaskToMember
    {
        public async Task<Result<TaskResponse>> AssignTaskToMember(AssignTaskToMemberRequest dto)
        {
            var project = await projectRepository.GetByIdAsync(dto.ProjectId);
            if (project is null)
                return Result<TaskResponse>.Failure(DomainErrors.Project.NotFound);
            var task = await taskRepository.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            var user = await userRepository.GetByIdAsync(dto.UserId);
            if (user is null)
                return Result<TaskResponse>.Failure(DomainErrors.User.NotFound);
            project.AssignTaskToMember(dto.UserId, dto.TaskId, clock);
            task.AssignTo(dto.UserId, clock);
            await projectRepository.UpdateAsync(project);
            await taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());

        }
    }
}
