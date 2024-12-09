namespace OpenAIPoC.API.Infrastructure.CosmosDB
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString();
    }
}