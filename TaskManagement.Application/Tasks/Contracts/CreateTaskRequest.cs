using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;
namespace TaskManagement.Application.Tasks.Contracts
{
    public record struct CreateTaskRequest(
            string Title,
            string Description,
            ProjectId ProjectId,
            UserId CreatedById,
            UserId? AssignedToId,
            Enum Priority,
            DateTime DueDate);
    
    
}
