using OpenAIPoC.API.Domain.Competitions;

namespace OpenAIPoC.API.Core.Competitions
{
    public class CompetitionCreationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Competition? Competition { get; set; }

        public CompetitionCreationResult(bool success, string message, Competition? competition)
        {
            Success = success;
            Message = message;
            Competition = competition;
        }
    }
}
