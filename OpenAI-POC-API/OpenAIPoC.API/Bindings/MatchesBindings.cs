using OpenAIPoC.API.Infrastructure.Repositories;
using OpenAIPoC.API.Domain.Matches;

public static class MatchesBindings
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IRepository<Match>, MatchesRepository>();

    }
}
