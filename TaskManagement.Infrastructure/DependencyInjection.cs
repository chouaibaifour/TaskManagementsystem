using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Users.ObjectValues;
using TaskManagement.Domain.Users.Repositories;
using TaskManagement.Infrastructure.Data.Json.Repositories;

namespace TaskManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            // JSON file persistence (for Users)
            var userFilePath = config["Storage:UsersFile"] ?? "users.json";
            services.AddSingleton<IUserRepository>(_ => new UserRepositoryJson(userFilePath));

            

            return services;
        }
    }
}
