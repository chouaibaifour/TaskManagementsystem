using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Projects.Abstractions;
using TaskManagement.Application.Tasks.Abstractions;
using TaskManagement.Application.Users.interfaces;
using TaskManagement.Infrastructure.AutoMapper;
using TaskManagement.Infrastructure.Data.Json.Repositories;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        // JSON file persistence (for Users)
        var userFilePath = config["Storage:UsersFile"] ?? "users.json";
        services.AddSingleton<IUserRepository>(_ => new UserRepositoryJson(userFilePath));
        // JSON file persistence (for Projects)
        var projectFilePath = config["Storage:ProjectsFile"] ?? "projects.json";
        services.AddSingleton<IProjectRepository>(_ => new ProjectRepositoryJson(projectFilePath));

        // JSON file persistence (for Tasks)
        var taskFilePath = config["Storage:TasksFile"] ?? "tasks.json";
        services.AddSingleton<ITaskRepository>(_ => new TaskRepositoryJson(taskFilePath));

        services.AddAutoMapper(cfg => { cfg.AddProfile<UserProfile>(); });

        return services;
    }
}