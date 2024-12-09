using MongoDB.Driver;
using OpenAIPoC.API.Infrastructure.CosmosDB;

public static class DatabaseBindings
{
    public static MongoDbContext Register(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        IConnectionStringProvider connectionStringProvider;
        IDbManagementClient adminClient;

        var databaseName = configuration["CosmosDb:DatabaseName"];
        if (environment.IsDevelopment())
        {
            string devConnectionString = configuration["CosmosDb:ConnectionString"];
            connectionStringProvider = new DevelopmentConnectionStringProvider(devConnectionString);
            adminClient = new DevelopmentDbManagementClient();
        }
        else
        {
            string keyVaultUrl = configuration["CosmosDb:KeyVaultUrl"];
            string secretName = configuration["CosmosDb:SecretName"];
            connectionStringProvider = new KeyVaultConnectionStringProvider(keyVaultUrl, secretName);
            var accountName = configuration["CosmosDb:AccountName"];
            var resourceGroupName = configuration["CosmosDb:ResourceGroupName"];
            adminClient = new MongoDbManagementClient(resourceGroupName, accountName, databaseName);

        }
        services.AddSingleton(connectionStringProvider);
        var connectionString = connectionStringProvider.GetConnectionString();
        var mongoDbClient = new MongoClient(connectionString);
        var database = mongoDbClient.GetDatabase(databaseName);
        services.AddSingleton(database);
        var context = new MongoDbContext
        {
            Database = database,
            AdminClient = adminClient,
        };

        return context;
    }
}
