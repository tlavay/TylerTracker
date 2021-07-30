using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using TylerTracker.Common.Models.Configuration;
using Microsoft.Azure.Cosmos;
using TylerTracker.Common.Repositories;
using TylerTracker.Common.Services.Interfaces;
using TylerTracker.Common.Services;

namespace TylerTracker.Common.Configuration
{
    public static class ConfigExtension
    {
        public static void ConfigureTylerTracker(this IServiceCollection services)
        {
            services.ConfigureCore();
            services.ConfigureRepos();
            services.ConfigureServices();
        }

        private static void ConfigureCore(this IServiceCollection services)
        {
            services.AddSingleton(p => ServiceFactory.CreateConfig(p.GetService<IConfiguration>()));
            services.AddSingleton(p => ServiceFactory.CreateDefaultAzureCredential());
            services.AddSingleton(p => ServiceFactory.CreateCosmosClient(p.GetService<DefaultAzureCredential>(), p.GetService<Config>()));
            services.AddSingleton(p => ServiceFactory.CreateCosmosContainer(p.GetService<CosmosClient>()));
        }

        private static void ConfigureRepos(this IServiceCollection services)
        {
            services.AddTransient<ITylerTrackerRepository, TylerTrackerRepository>();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IHealthService, HealthService>();
        }
    }
}
