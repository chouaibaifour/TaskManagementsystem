

namespace TaskManagement.Application.Projects.Contracts.Member
{
    public record struct CreateUpdateProjectMemberRequest(Guid ProjectId, Guid UserId,Enum MemberRole);
}
