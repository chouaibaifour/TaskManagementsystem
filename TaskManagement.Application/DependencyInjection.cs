using Microsoft.Extensions.DependencyInjection;

using TaskManagement.Application.Users.Abstractions;
using TaskManagement.Application.Users.UseCase;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Application.Projects.UseCase;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Users.Policies;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Tasks.UserCase;

namespace TaskManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            services.AddScoped<IClock, DefaultClockService>();
            services.AddScoped<IPasswordPolicy, DefaultPasswordPolicy>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();
            

            return services;
        }
    }
}
