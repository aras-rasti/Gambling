using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gambling.Data.Contracts;
using Gambling.Data.Repositories;
using Gambling.Services.Contracts;
using Gambling.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gambling.WebFramework.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IApplicationUserManager, ApplicationUserManager>();
        }
    }
}
