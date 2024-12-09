using OpenAIPoC.API.Core.Teams.Dtos;
using OpenAIPoC.API.Domain.Matches;
using OpenAIPoC.API.Domain.Teams;

namespace OpenAIPoC.API.Core.Matches
{
    public class MatchCreateManager
    {
        private readonly IRepository<Match> _matchesRepository;

        public MatchCreateManager(IRepository<Match> matchesRepository)
        {
            _matchesRepository = matchesRepository;
        }

    }
}
