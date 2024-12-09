namespace OpenAIPoC.API.Infrastructure.CosmosDB
{
    public interface IDbManagementClient
    {
        public Task CreateCollectionAsync(string collectionName, string shardKey);
    }
}
