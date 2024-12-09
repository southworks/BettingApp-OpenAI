using System.ComponentModel.DataAnnotations;

namespace OpenAIPoC.API.Core.Teams.Dtos
{
    public record MatchWithLineupDto : MatchDto
    {
        [Required]
        public required List<PlayerDto> HomeStarters { get; init; }

        [Required]
        public required List<PlayerDto> AwayStarters { get; init; }

        [Required]
        public required List<PlayerDto> HomeSubstitutes { get; init; }

        [Required]
        public required List<PlayerDto> AwaySubstitutes { get; init; }
    }
}
