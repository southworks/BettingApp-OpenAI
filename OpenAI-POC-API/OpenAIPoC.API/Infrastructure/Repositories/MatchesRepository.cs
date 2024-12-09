using OpenAIPoC.API.Domain.Matches;
using MongoDB.Driver;

namespace OpenAIPoC.API.Infrastructure.Repositories
{
    public class MatchesRepository(IMongoDatabase mongoDatabase) : RepositoryBase<Match>(mongoDatabase, "matches")
    {
    }
}
