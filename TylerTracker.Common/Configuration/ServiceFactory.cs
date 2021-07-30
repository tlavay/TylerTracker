using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TylerTracker.Common.Models.Configuration;

namespace TylerTracker.Common.Configuration
{
    internal static class ServiceFactory
    {
        public static Config CreateConfig(IConfiguration configuration)
        {
            return configuration.GetSection("Config").Get<Config>();
        }

        public static DefaultAzureCredential CreateDefaultAzureCredential()
        {
            return new DefaultAzureCredential();
        }

        public static CosmosClient CreateCosmosClient(DefaultAzureCredential defaultAzureCredential, Config config)
        {
            if (string.IsNullOrEmpty(config?.Cosmos?.DocumentEndpoint))
            {
                throw new ConfigurationErrorsException("The config value: Config:Cosmos:DocumentEndpoint was not present.");
            }

            var clientOptions = new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase },
                AllowBulkExecution = true,
            };

            return new CosmosClient(config.Cosmos.DocumentEndpoint, "4LDqd90Cck5tI9CcN8VGXkDcPGESP95mKWErtfwhSxrlvWsJxo2g2lG1mll8dSgCyMg1fcSLmf7uRGyOQCIj6A==", clientOptions);
        }

        public static Container CreateCosmosContainer(CosmosClient cosmosClient)
        {
            return cosmosClient.GetContainer("tyler-tracker", "document-container");
        }
    }
}
