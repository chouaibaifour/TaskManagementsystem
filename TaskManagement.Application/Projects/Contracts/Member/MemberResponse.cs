namespace TaskManagement.Application.Projects.Contracts.Member
{
    public record struct MemberResponse(
        Guid UserId,
        string Role,
        string IsActive
        );


}
