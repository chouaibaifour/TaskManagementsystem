using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Users.Abstractions;
using TaskManagement.Infrastructure.AutoMapper;
using TaskManagement.Infrastructure.Data.Json.FileHandling;
using TaskManagement.Infrastructure.Data.Json.Repositories;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        // JSON file persistence (for Users)

        services.AddSingleton(new UserFilePath(config["Storage:UsersFile"] ?? "users.json"));
        services.AddSingleton<IUserRepository, UserRepositoryJson>();
        // JSON file persistence (for Projects)
        
        services.AddSingleton(new ProjectFilePath(config["Storage:ProjectsFile"] ?? "projects.json"));
        services.AddSingleton<IProjectRepository,ProjectRepositoryJson>();

        // JSON file persistence (for Tasks)
        
        services.AddSingleton(new TaskFilePath(config["Storage:TasksFile"] ?? "tasks.json"));
        services.AddSingleton<ITaskRepository,TaskRepositoryJson>( );

        services.AddAutoMapper(_ => { }, typeof(UserProfile),typeof(ProjectProfile)  /*, ...*/);

        return services;
    }
}