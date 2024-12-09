using OpenAIPoC.API.Core.Teams;
using OpenAIPoC.API.Infrastructure.Repositories;
using OpenAIPoC.API.Domain.Teams;

public static class TeamsBindings
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<ITeamsManager, TeamsManager>();
        services.AddScoped<ITeamsLineupPromptBuilder, TeamsLineupPromptBuilder>();
        services.AddScoped<IRepository<Team>, TeamsRepository>();
        services.AddScoped<ITeamsMatchWinnerManager, TeamsMatchWinnerManager>();
        services.AddScoped<ITeamsMatchWinnerPromptBuilder, TeamsMatchWinnerPromptBuilder>();
    }
}
