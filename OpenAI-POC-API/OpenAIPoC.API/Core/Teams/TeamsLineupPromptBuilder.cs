using OpenAI.Chat;
using OpenAIPoC.API.Core.Teams.Dtos;
using System.Text.Json;

namespace OpenAIPoC.API.Core.Teams
{
    public class TeamsLineupPromptBuilder : ITeamsLineupPromptBuilder
    {
        public ChatMessage[] BuildLineupPrompt(MatchDto lineupPromptDto)
        {
            string systemMessage = $@"You are a football expert. You will receive match information, including the squads for two teams and the competition type. 
                Your task is to select and return 16 players for each team in total, divided into 11 starting players (homeStarters or awayStarters) and 5 substitutes (homeSubstitutes or awaySubstitutes).
                Sample request: 
                {{
                    ""homeTeam"": [{{""name"":""homePlayer1"", ""position"": ""Defender""}}, ..., {{""name"":""homePlayerN"", ""position"":""Forward""}}],
                    ""awayTeam"": [{{""name"":""awayPlayer1"", ""position"": ""Forward""}}, ..., {{""name"":""homePlayerM"", ""position"":""Forward""}}],
                    ""competition"": ""league""
                }}
                Response format: Return a JSON object strictly formatted as follows, ensuring 11 starters and 5 substitutes for each team:
                {{
                    ""homeStarters"": [{{""name"":""homePlayer1"", ""position"": ""Defender""}}, ..., {{""name"":""homePlayer11"", ""position"":""Forward""}}],
                    ""homeSubstitutes:"":[{{""name"": ""homePlayer12"", ""position"": ""Substitute""}}, ..., {{""name"":""homePlayer16"", ""position"":""Substitute""}}],
                    ""awayStarters"": [{{""name"":""awayPlayer1"", ""position"": ""Forward""}}, ..., {{""name"":""homePlayer11"", ""position"":""Forward""}}],
                    ""awaySubstitutes:"":[{{""name"": ""awayPlayer12"", ""position"": ""Substitute""}}, ..., {{""name"":""awayPlayer16"", ""position"":""Substitute""}}],
                }}
                Important: 
                - Ensure no trailing commas in the JSON.
                - Do not include any extra text or explanations outside the JSON format.
                - Return a single JSON object as specified above.";

            var prompt = new ChatMessage[]
            {
                new SystemChatMessage(systemMessage),
                new UserChatMessage(JsonSerializer.Serialize(lineupPromptDto))
            };

            return prompt;
        }
    }
}
