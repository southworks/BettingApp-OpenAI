using System.Text.Json.Serialization;

namespace OpenAIPoC.API.Core.Teams.Dtos
{
    public record MatchWinnerResponseDto
    {
        [JsonPropertyName("homeWin")]
        public string HomeWin { get; set; } = string.Empty;
        [JsonPropertyName("draw")]
        public string Draw { get; set; } = string.Empty;
        [JsonPropertyName("awayWin")]
        public string AwayWin { get; set; } = string.Empty;
    }

}
