using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Spec.Contracts
{
    public record struct AssignTaskToMemberRequest(
        Guid ProjectId,
        Guid TaskId,
        Guid UserId
        );

}
