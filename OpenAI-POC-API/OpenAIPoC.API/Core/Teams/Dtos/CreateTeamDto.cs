namespace OpenAIPoC.API.Core.Teams.Dtos
{
    public class CreateTeamDto
    {
        public required string Name { get; set; }
        public required List<PlayerDto> Squad { get; set; }
    }
}
