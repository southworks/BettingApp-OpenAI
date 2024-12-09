using OpenAIPoC.API.Core.AI;
using OpenAIPoC.API.Core.Teams.Dtos;
using OpenAIPoC.API.Domain.Competitions;
using OpenAIPoC.API.Domain.Matches;
using OpenAIPoC.API.Domain.Teams;
using System.Text.Json;
using OpenAI.Chat;

namespace OpenAIPoC.API.Core.Teams
{
    public class TeamsMatchWinnerManager : ITeamsMatchWinnerManager
    {
        private readonly ITeamsMatchWinnerPromptBuilder _promptBuilder;
        private readonly IOpenAIService _openAIService;
        private readonly IRepository<Match> _matchesRepository;
        private readonly IRepository<Team> _teamsRepository;
        private readonly IRepository<Competition> _competitionsRepository;
        private static readonly Options.JsonOptions _jsonOptions = new Options.JsonOptions();
        private const int MaxRetries = 3;

        public TeamsMatchWinnerManager(ITeamsMatchWinnerPromptBuilder promptBuilder, IOpenAIService openAIService, IRepository<Match> matchesRepository, IRepository<Team> teamsRepository, IRepository<Competition> competitionsRepository)
        {
            _promptBuilder = promptBuilder;
            _openAIService = openAIService;
            _matchesRepository = matchesRepository;
            _teamsRepository = teamsRepository;
            _competitionsRepository = competitionsRepository;
        }

        public async Task<MatchWinnerResponseDto> PredictMatchWinner(MatchPredictionDto matchDto)
        {
            MatchWinnerResponseDto? result = null;
            ChatCompletion response = null; 

            for (int attempt = 0; attempt < MaxRetries && result == null; attempt++)
            {
                try
                {
                    var prompt = _promptBuilder.BuildMatchWinnerPrompt(matchDto);
                    response = await _openAIService.GetChatCompletion(prompt);
                    var matchWinnerDto = JsonSerializer.Deserialize<MatchWinnerResponseDto>(response.Content[0].Text, _jsonOptions.SerializerOptions);

                    var winner = float.Parse(matchWinnerDto.HomeWin) < float.Parse(matchWinnerDto.AwayWin) ? matchDto.HomeTeam : matchDto.AwayTeam;
                    var match = new Match
                    {
                        HomeTeam = matchDto.HomeTeam,
                        AwayTeam = matchDto.AwayTeam,
                        Competition = matchDto.Competition,
                        HomeStarters = MapPlayerList(matchDto.HomeStarters),
                        AwayStarters = MapPlayerList(matchDto.AwayStarters),
                        HomeSubstitutes = MapPlayerList(matchDto.HomeSubstitutes),
                        AwaySubstitutes = MapPlayerList(matchDto.AwaySubstitutes),
                        PredictedWinner = winner,
                        PredictionDate = DateTime.UtcNow,
                        HomeWinOdd = matchWinnerDto.HomeWin,
                        AwayWinOdd = matchWinnerDto.AwayWin,
                        DrawOdd = matchWinnerDto.Draw,
                    };

                    await _matchesRepository.AddAsync(match);

                    result = matchWinnerDto;
                }
                catch (Exception ex)
                {
                    // We should log the exception
                    Console.WriteLine($"Attempt {attempt + 1} failed with error: {ex.Message}");
                    if (response != null) {
                        Console.WriteLine($"ChatGPT Response: {response.Content[0].Text}");
                    }                    
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
            }
            return result;
        }

        private List<Player> MapPlayerList(List<PlayerDto> playerDtoList)
        {
            return playerDtoList.Select(playerDto => new Player
            {
                Name = playerDto.Name,
                Position = playerDto.Position
            }).ToList();
        }
    }
}
