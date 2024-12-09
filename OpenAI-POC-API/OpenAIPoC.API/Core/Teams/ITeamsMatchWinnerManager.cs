using OpenAIPoC.API.Core.Teams.Dtos;

namespace OpenAIPoC.API.Core.Teams
{
    public interface ITeamsMatchWinnerManager
    {
        public Task<MatchWinnerResponseDto> PredictMatchWinner(MatchPredictionDto match);
    }
}
