using MongoDB.Driver;

namespace OpenAIPoC.API.Infrastructure.CosmosDB
{
    public record MongoDbContext
    {
        public required IMongoDatabase Database { get; init; }
        public required IDbManagementClient AdminClient { get; init; }
    }
}
