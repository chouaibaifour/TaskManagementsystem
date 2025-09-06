
using TaskManagement.Application.Projects.Contracts.Member;

namespace TaskManagement.Application.Projects.Contracts
{
    public record struct ProjectResponse(
        Guid Id,
        string Name,
        string Description,
        Guid OwnerId,
        string Status,
        List<MemberResponse> Members
    );

}
