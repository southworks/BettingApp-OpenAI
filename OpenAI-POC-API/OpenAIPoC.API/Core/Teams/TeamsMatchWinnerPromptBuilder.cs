using OpenAI.Chat;
using OpenAIPoC.API.Core.Teams.Dtos;

namespace OpenAIPoC.API.Core.Teams
{
    public class TeamsMatchWinnerPromptBuilder : ITeamsMatchWinnerPromptBuilder
    {
        public ChatMessage[] BuildMatchWinnerPrompt(MatchPredictionDto matchWinnerPrompt)
        {
            List<PlayerDto> homeLineUp = [.. matchWinnerPrompt.HomeStarters, .. matchWinnerPrompt.HomeSubstitutes];
            List<PlayerDto> awayLineUp = [.. matchWinnerPrompt.AwayStarters, .. matchWinnerPrompt.AwaySubstitutes];
            string systemMessage = @"You are a football betting expert specializing in calculating odds for the match winner 3-way market.
            You will receive detailed match information, including team lineups, competition, and an overround. 
            Your task is to provide the betting odds for the match's '1X2' market in decimal format.
            Also apply the overround provided on top of the calculated odds for producing the final prices for this market.
            Response format: You must return only a JSON object strictly formatted as follows:
            {
                ""homeWin"": ""<decimal>"",
                ""draw"": ""<decimal>"",
                ""awayWin"": ""<decimal>""
            }
            Replace <decimal> with the calculated odds in decimal format (e.g., 2.50, 3.30, etc.).
            Do not include any explanations, comments, or text outside the JSON object.

            Example response:
            {
                ""homeWin"": ""2.30"",
                ""draw"": ""3.20"",
                ""awayWin"": ""3.00""
            }
            Important:
            - Ensure no trailing commas in the JSON.
            - Do not include any extra text or explanations outside the JSON format.
            - Return a single JSON object as specified above.";

            string userMessage = $@"This is a {matchWinnerPrompt.Competition} match between {matchWinnerPrompt.HomeTeam} and {matchWinnerPrompt.AwayTeam}. The initial lineups for each team are:
            {matchWinnerPrompt.HomeTeam}: {string.Join(", ", homeLineUp.ConvertAll(p => p.Name))}.
            {matchWinnerPrompt.AwayTeam}: {string.Join(", ", awayLineUp.ConvertAll(p => p.Name))}.
            The odds should account for factors such as: League or competition position, favorite and underdog, win/loss/draw statistics, recent form, home advantage, key players and any other relevant data provided in the match input.
            Take into account an overround of {matchWinnerPrompt.BettingMargin}% for this market.";

            var prompt = new ChatMessage[]
            {
                new SystemChatMessage(systemMessage),
                new UserChatMessage(userMessage)
            };

            return prompt;
        }
    }
}
