using System.ComponentModel.DataAnnotations;

namespace OpenAIPoC.API.Core.Teams.Dtos
{
    public record MatchDto
    {
        [Required]
        public required List<PlayerDto> HomeTeam { get; init; }

        [Required]
        public required List<PlayerDto> AwayTeam { get; init; }

        [Required]
        public required string Competition { get; init; }
    }
}
