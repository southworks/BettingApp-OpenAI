using OpenAIPoC.API.Domain.Matches;

namespace OpenAIPoC.API.Core.Matches
{
    public class MatchCreationResult
    {
        public bool Success { get; }
        public string Message { get; }
        public Match? Match { get; }

        public MatchCreationResult(bool success, string message, Match? match)
        {
            Success = success;
            Message = message;
            Match = match;
        }
    }
}
