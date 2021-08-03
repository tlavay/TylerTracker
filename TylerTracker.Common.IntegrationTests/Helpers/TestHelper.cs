using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text.Json;
using TylerTracker.Common.Configuration;

namespace TylerTracker.Common.IntegrationTests.Helpers
{
    public class TestHelper
    {
        private static ServiceProvider serviceProvider;
        public static ServiceProvider GetServiceProvider()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddUserSecrets<TestHelper>().Build();
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.ConfigureTylerTracker();
            return services.BuildServiceProvider();
        }

        public static TResult LoadFile<TResult>(string filePath)
        {
            return JsonSerializer.Deserialize<TResult>(File.ReadAllText(filePath));
        }

        //I know this is bad code but its good enough to help some tests.
        public static T GetService<T>()
        {
            if (serviceProvider == null)
            {
                serviceProvider = GetServiceProvider();
            }

            return serviceProvider.GetService<T>();
        }
    }
}
