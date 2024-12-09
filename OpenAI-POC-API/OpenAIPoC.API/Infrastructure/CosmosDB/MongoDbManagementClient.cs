
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.CosmosDB;
using Azure.ResourceManager.CosmosDB.Models;


namespace OpenAIPoC.API.Infrastructure.CosmosDB
{
    public class MongoDbManagementClient : IDbManagementClient
    {
        private readonly MongoDBDatabaseResource _database;

        public MongoDbManagementClient(string resourceGroupName, string cosmosDbAccountName, string databaseName)
        {
            ArmClient client = new ArmClient(new DefaultAzureCredential());
            var subscription = client.GetDefaultSubscription();
            var cosmosDbAccountId = new ResourceIdentifier(
                $"{subscription.Id}/resourceGroups/{resourceGroupName}/providers/Microsoft.DocumentDB/databaseAccounts/{cosmosDbAccountName}"
            );
            var cosmosDbAccount = client.GetCosmosDBAccountResource(cosmosDbAccountId);
            _database = cosmosDbAccount.GetMongoDBDatabase(databaseName).Value;
        }
        public async Task CreateCollectionAsync(string collectionName, string shardKey)
        {
            var collections = _database.GetMongoDBCollections();
            var collectionInfo = new MongoDBCollectionResourceInfo(collectionName);
            collectionInfo.ShardKey.Add(shardKey, "hashed");
            var collectionContent = new MongoDBCollectionCreateOrUpdateContent(AzureLocation.WestUS, collectionInfo);
            await collections.CreateOrUpdateAsync(Azure.WaitUntil.Completed, collectionName, collectionContent);
        }
    }
}
