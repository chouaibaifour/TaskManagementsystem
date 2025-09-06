

namespace TaskManagement.Application.Projects.Contracts
{
    public record struct CreateProjectRequest(
        string Name,
        string Description,
        Guid OwnerId
        );

}
