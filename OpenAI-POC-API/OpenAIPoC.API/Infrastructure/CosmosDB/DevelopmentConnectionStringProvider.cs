namespace OpenAIPoC.API.Infrastructure.CosmosDB
{
    public class DevelopmentConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _connectionString;

        public DevelopmentConnectionStringProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}