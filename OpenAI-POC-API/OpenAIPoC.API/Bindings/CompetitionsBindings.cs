using OpenAIPoC.API.Infrastructure.Repositories;
using OpenAIPoC.API.Domain.Competitions;
using OpenAIPoC.API.Core.Competitions;
using OpenAIPoC.API.Core.Matches;

public static class CompetitionsBindings
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IRepository<Competition>, CompetitionsRepository>();
        services.AddScoped<CompetitionCreateManager>();
        services.AddScoped<MatchCreateManager>();
    }
}
