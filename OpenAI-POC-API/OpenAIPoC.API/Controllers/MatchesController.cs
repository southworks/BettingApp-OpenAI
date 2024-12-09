using Microsoft.AspNetCore.Mvc;
using OpenAIPoC.API.Domain.Matches;

namespace OpenAIPoC.API.Controllers
{
    [Route("matches")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IRepository<Match> _matchesRepository;

        public MatchesController(IRepository<Match> matchesRepository)
        {
            _matchesRepository = matchesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            var matches = await _matchesRepository.GetAllAsync();

            if (matches == null || !matches.Any())
            {
                return NotFound("No matches found.");
            }

            return Ok(matches);
        }

    }
}
