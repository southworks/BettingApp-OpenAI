using OpenAIPoC.API.Domain.Competitions;
using OpenAIPoC.API.Core.Competitions.Dtos;

namespace OpenAIPoC.API.Core.Competitions
{
    public class CompetitionCreateManager
    {
        private readonly IRepository<Competition> _competitionsRepository;

        public CompetitionCreateManager(IRepository<Competition> competitionsRepository)
        {
            _competitionsRepository = competitionsRepository;
        }

        public async Task<CompetitionCreationResult> CreateCompetitionAsync(CompetitionDto competitionDto)
        {
            var existingCompetition = (await _competitionsRepository.GetAllAsync())
                .FirstOrDefault(c => c.Name == competitionDto.Name);

            if (existingCompetition != null)
            {
                return new CompetitionCreationResult(false, "A competition with the same name already exists in the database.", null);
            }

            var competition = new Competition
            {
                Name = competitionDto.Name
            };

            await _competitionsRepository.AddAsync(competition);
            return new CompetitionCreationResult(true, "Competition created successfully.", competition);
        }
    }
}
