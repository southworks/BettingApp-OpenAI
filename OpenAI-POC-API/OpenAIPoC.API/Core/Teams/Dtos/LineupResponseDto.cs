namespace OpenAIPoC.API.Core.Teams.Dtos
{
    public record LineupResponseDto
    {
        public required List<PlayerDto> HomeStarters { get; init; }

        public required List<PlayerDto> AwayStarters { get; init; }

        public required List<PlayerDto> HomeSubstitutes { get; init; }

        public required List<PlayerDto> AwaySubstitutes { get; init; }
    }
}
