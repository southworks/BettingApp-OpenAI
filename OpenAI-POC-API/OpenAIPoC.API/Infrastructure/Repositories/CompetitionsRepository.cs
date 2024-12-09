using OpenAIPoC.API.Domain.Competitions;
using MongoDB.Driver;

namespace OpenAIPoC.API.Infrastructure.Repositories
{
    public class CompetitionsRepository(IMongoDatabase mongoDatabase) : RepositoryBase<Competition>(mongoDatabase, "competitions")
    {
    }
}
