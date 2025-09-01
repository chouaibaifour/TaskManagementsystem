using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TaskManagement.Application.Common;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Application.Spec.Abstractions;
using TaskManagement.Application.Spec.Contracts;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Tasks.Contracts;
using TaskManagement.Application.Tasks.Mapper;
using TaskManagement.Application.Users.interfaces;
using TaskManagement.Domain.Common.Errors;
using TaskManagement.Domain.Common.Services;

namespace TaskManagement.Application.Spec.UserCase
{
    public class AssignTaskToMemberSpec : IAssignTaskToMember
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClock _clock;

        public AssignTaskToMemberSpec(IProjectRepository projectRepository, ITaskRepository taskRepository, IUserRepository userRepository, IClock clock)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _clock = clock;
        }

        public async Task<Result<TaskResponse>> AssignTaskToMember(AssignTaskToMemberRequest dto)
        {
            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);
            if (project is null)
                return Result<TaskResponse>.Failure(DomainErrors.Project.NotFound);
            var task = await _taskRepository.GetByIdAsync(dto.TaskId);
            if (task is null)
                return Result<TaskResponse>.Failure(DomainErrors.Task.NotFound);
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user is null)
                return Result<TaskResponse>.Failure(DomainErrors.User.NotFound);
            project.AssignTaskToMember(dto.UserId, dto.TaskId, _clock);
            task.AssignTo(dto.UserId, _clock);
            await _projectRepository.UpdateAsync(project);
            await _taskRepository.UpdateAsync(task);
            return Result<TaskResponse>.Success(task.ToDto());

        }
    }
}
