
namespace TaskManagement.Application.Tasks.Abstractions
{
    public interface ITaskRepository
    {
       Task<bool> IsProjectTaskExistByTitle(Guid projectId, string title);
       Task AddAsync(Domain.Tasks.Task task);
        Task<Domain.Tasks.Task?> GetByIdAsync(Guid taskId);
        Task UpdateAsync(Domain.Tasks.Task task);
        Task<bool> DeleteAsync(Guid taskId);
        Task<List<Domain.Tasks.Task>> ListTasksByProjectIdAsync(Guid projectId);
        

    }
}
