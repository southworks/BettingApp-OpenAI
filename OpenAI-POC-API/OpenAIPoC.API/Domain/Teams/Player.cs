namespace OpenAIPoC.API.Domain.Teams
{
    public record Player
    {
        public required string Name { get; init; }
        public required string Position { get; init; }
    }
}
