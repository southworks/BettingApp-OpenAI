
namespace OpenAIPoC.API.Infrastructure.CosmosDB
{
    public class DevelopmentDbManagementClient : IDbManagementClient
    {
        public Task CreateCollectionAsync(string collectionName, string shardKey)
        {
            // Doing nothing will force Mongo driver to create collections
            return Task.CompletedTask;
        }
    }
}
