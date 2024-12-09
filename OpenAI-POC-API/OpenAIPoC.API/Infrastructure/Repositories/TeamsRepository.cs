using OpenAIPoC.API.Domain.Teams;
using MongoDB.Driver;

namespace OpenAIPoC.API.Infrastructure.Repositories
{
    public class TeamsRepository(IMongoDatabase mongoDatabase) : RepositoryBase<Team>(mongoDatabase, "teams")
    {
    }
}
