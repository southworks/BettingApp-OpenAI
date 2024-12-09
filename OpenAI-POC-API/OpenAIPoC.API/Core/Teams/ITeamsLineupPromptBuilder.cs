using OpenAI.Chat;
using OpenAIPoC.API.Core.Teams.Dtos;

namespace OpenAIPoC.API.Core.Teams
{
    public interface ITeamsLineupPromptBuilder
    {
        public ChatMessage[] BuildLineupPrompt(MatchDto lineupPrompt);
    }
}
