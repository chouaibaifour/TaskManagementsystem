

using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Tasks.ValueObjects;

namespace TaskManagement.Application.Tasks.Abstractions
{
    public interface ITaskRepository
    {
       Task<bool> IsProjectTaskExistByTitle(ProjectId projectId, Title title);
       Task AddAsync(Domain.Tasks.Task task);
        Task<Domain.Tasks.Task?> GetByIdAsync(TaskId taskId);
        Task UpdateAsync(Domain.Tasks.Task task);
        Task<bool> DeleteAsync(TaskId taskId);
        Task<List<Domain.Tasks.Task>> ListTasksByProjectIdAsync(ProjectId projectId);
        

    }
}
