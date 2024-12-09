using OpenAI.Chat;
using OpenAIPoC.API.Core.Teams.Dtos;

namespace OpenAIPoC.API.Core.Teams
{
    public interface ITeamsMatchWinnerPromptBuilder
    {
        public ChatMessage[] BuildMatchWinnerPrompt(MatchPredictionDto matchWinnerPrompt);
    }
}
