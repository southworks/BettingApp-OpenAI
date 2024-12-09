using OpenAIPoC.API.Core.AI;
using OpenAIPoC.API.Core.Teams.Dtos;
using OpenAIPoC.API.Domain.Competitions;
using OpenAIPoC.API.Domain.Teams;
using System.Text.Json;

namespace OpenAIPoC.API.Core.Teams
{
    public class TeamsManager : ITeamsManager
    {
        private readonly ITeamsLineupPromptBuilder _promptBuilder;
        private readonly IOpenAIService _openAIService;
        private readonly IRepository<Team> _teamsRepository;
        private readonly IRepository<Competition> _competitionsRepository;
        private static readonly Options.JsonOptions _jsonOptions = new Options.JsonOptions();
        private const int MaxRetries = 3;

        public TeamsManager(ITeamsLineupPromptBuilder promptBuilder, IOpenAIService openAIService, IRepository<Team> teamsRepository, IRepository<Competition> competitionsRepository)
        {
            _promptBuilder = promptBuilder;
            _openAIService = openAIService;
            _teamsRepository = teamsRepository;
            _competitionsRepository = competitionsRepository;
        }

        public async Task<MatchWithLineupDto?> PredictLineUpsAsync(MatchDto match)
        {
            for (int attempt = 0; attempt < MaxRetries; attempt++)
            {
                try {
                    var prompt = _promptBuilder.BuildLineupPrompt(match);
                    var response = await _openAIService.GetChatCompletion(prompt);

                    var lineupResponse = JsonSerializer.Deserialize<LineupResponseDto>(response.Content[0].Text, _jsonOptions.SerializerOptions);
                    
                    if (!HasDuplicatePlayers(lineupResponse.HomeStarters, lineupResponse.HomeSubstitutes) &&
                        !HasDuplicatePlayers(lineupResponse.AwayStarters, lineupResponse.AwaySubstitutes))
                    {
                        return new MatchWithLineupDto {
                            HomeTeam = match.HomeTeam,
                            AwayTeam = match.AwayTeam,
                            Competition = match.Competition,
                            HomeStarters = lineupResponse.HomeStarters,
                            AwayStarters = lineupResponse.AwayStarters,
                            HomeSubstitutes = lineupResponse.HomeSubstitutes,
                            AwaySubstitutes = lineupResponse.AwaySubstitutes
                        };
                    }
                }
                catch (Exception)
                {
                    if (attempt == MaxRetries  - 1)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        private static bool HasDuplicatePlayers(List<PlayerDto> starters, List<PlayerDto> substitutes)
        {
            return starters
                .Select(player => NormalizePlayerName(player.Name))
                .Intersect(substitutes.Select(player => NormalizePlayerName(player.Name)))
                .Any();
        }

        private static string NormalizePlayerName(string name)
        {
            return name.Trim().ToLowerInvariant();
        }
    }
}
