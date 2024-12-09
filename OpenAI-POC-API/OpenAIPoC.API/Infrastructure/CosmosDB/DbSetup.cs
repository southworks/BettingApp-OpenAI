using MongoDB.Driver;
using OpenAIPoC.API.Domain.Competitions;
using OpenAIPoC.API.Domain.Matches;
using OpenAIPoC.API.Domain.Teams;
using OpenAIPoC.API.Infrastructure.CosmosDB;
using System.Text.Json;

public class DbSetup(MongoDbContext dbContext)
{
    private readonly IMongoDatabase _database = dbContext.Database;
    private readonly IDbManagementClient _adminClient = dbContext.AdminClient;

    public async Task InitializeAsync(IHostEnvironment environment)
    {
        await CreateAndPopulateCollectionAsync<Team>(environment, "teams", "Teams.json");
        await CreateAndPopulateCollectionAsync<Match>(environment, "matches", "Matches.json");
        await CreateAndPopulateCollectionAsync<Competition>(environment, "competitions", "Competitions.json");
    }

    private async Task CreateAndPopulateCollectionAsync<T>(IHostEnvironment environment, string collectionName, string jsonFileName)
    {
        var collection = await CreateCollectionIfNotExistsAsync<T>(collectionName);
        await PopulateCollectionIfEmptyAsync<T>(environment, collection, jsonFileName);
    }

    private async Task PopulateCollectionIfEmptyAsync<T>(IHostEnvironment environment, IMongoCollection<T> collection, string jsonFileName)
    {
        var count = await collection.CountDocumentsAsync(FilterDefinition<T>.Empty);
        if (count != 0)
        {
            return;
        }
        var _filePath = Path.Combine(environment.ContentRootPath, "Infrastructure", "Data", jsonFileName);
        var data = LoadDataFromJson<T>(_filePath);
        if (data != null)
        {
            await collection.InsertManyAsync(data);
        }
    }

    private async Task<IMongoCollection<T>> CreateCollectionIfNotExistsAsync<T>(string collectionName)
    {
        if (await DoesCollectionExistAsync(collectionName))
        {
            return _database.GetCollection<T>(collectionName);
        }

        var collection = await CreateCollectionAsync<T>(collectionName);
        return collection;
    }

    private List<T>? LoadDataFromJson<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        var jsonData = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(jsonData);
    }

    private async Task<bool> DoesCollectionExistAsync(string collectionName)
    {
        var collectionList = await _database.ListCollectionNamesAsync();
        var collections = await collectionList.ToListAsync();

        return collections.Contains(collectionName);
    }

    private async Task<IMongoCollection<T>> CreateCollectionAsync<T>(string collectionName)
    {
        await _adminClient.CreateCollectionAsync(collectionName, "_id");

        return _database.GetCollection<T>(collectionName);
    }
}
