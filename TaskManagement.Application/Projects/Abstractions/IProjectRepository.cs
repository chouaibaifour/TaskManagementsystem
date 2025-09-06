using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Primitives;
using TaskManagement.Domain.Projects;
using TaskManagement.Domain.Projects.ValueObjects;
using TaskManagement.Domain.Users.ValueObjects;

namespace TaskManagement.Application.Projects.Abstractions
{
    public interface IProjectRepository
    {
        Task<bool> IsProjectExistsByNameAsync(string name);
        Task<bool> IsProjectExistsAsync(Guid  projectId);
        Task<Project?> GetByIdAsync(Guid  projectId);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task<bool> DeleteAsync(Guid  projectId);
        Task<List<Project>> ListAllProjectsAsync();
        Task<List<Project>> ListProjectsByOwnerIdAsync(Guid ownerId);
        
    }

    
}
