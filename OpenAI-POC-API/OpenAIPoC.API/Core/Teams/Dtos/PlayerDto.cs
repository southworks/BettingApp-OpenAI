namespace OpenAIPoC.API.Core.Teams.Dtos
{
    public record PlayerDto
    {
        public required string Name { get; init; }

        public required string Position { get; init; }
    }

}
