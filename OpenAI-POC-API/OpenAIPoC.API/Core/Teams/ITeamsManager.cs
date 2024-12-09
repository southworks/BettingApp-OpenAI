using OpenAIPoC.API.Core.Teams.Dtos;

namespace OpenAIPoC.API.Core.Teams
{
    public interface ITeamsManager
    {
        public Task<MatchWithLineupDto?> PredictLineUpsAsync(MatchDto match);
    }
}
