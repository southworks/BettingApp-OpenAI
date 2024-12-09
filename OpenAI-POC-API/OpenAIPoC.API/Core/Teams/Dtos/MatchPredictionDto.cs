using System.ComponentModel.DataAnnotations;

namespace OpenAIPoC.API.Core.Teams.Dtos
{
    public record MatchPredictionDto
    {
        [Required]
        public required string HomeTeam { get; init; }

        [Required]
        public required string AwayTeam { get; init; }

        [Required]
        public required List<PlayerDto> HomeStarters { get; init; }

        [Required]
        public required List<PlayerDto> AwayStarters { get; init; }
        [Required]
        public required List<PlayerDto> HomeSubstitutes { get; init; }

        [Required]
        public required List<PlayerDto> AwaySubstitutes { get; init; }

        [Required]
        public required string Competition { get; init; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "BettingMargin must be greater than 0")]
        public required decimal BettingMargin { get; init; }
    }
}
