using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common.Services;
using TaskManagement.Domain.Users.Policies;

namespace TaskManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            services.AddScoped<IClock, DefaultClockService>();
            services.AddScoped<IPasswordPolicy, DefaultPasswordPolicy>();
            return services;
        }
    }
}
