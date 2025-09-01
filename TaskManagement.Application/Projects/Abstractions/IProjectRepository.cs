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
        Task<bool> IsProjectExistsByNameAsync(ProjectName name);
        Task<bool> IsProjectExistsAsync(ProjectId  projectId);
        Task<Project?> GetByIdAsync(ProjectId  projectId);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task<bool> DeleteAsync(ProjectId  projectId);
        Task<List<Project>> ListAllProjectsAsync();
        Task<List<Project>> ListProjectsByOwnerIdAsync(UserId ownerId);
        
    }

    
}
