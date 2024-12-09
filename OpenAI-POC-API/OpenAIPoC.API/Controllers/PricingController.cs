using Microsoft.AspNetCore.Mvc;
using OpenAIPoC.API.Core.Teams;
using OpenAIPoC.API.Core.Teams.Dtos;

namespace OpenAIPoC.API.Controllers
{
    [Route("pricing")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly ITeamsMatchWinnerManager _teamsMatchWinnerManager;

        public PricingController(ITeamsMatchWinnerManager teamsLineupManager)
        {
            _teamsMatchWinnerManager = teamsLineupManager;
        }

        [HttpPost("match-winner")]
        public async Task<IActionResult> CalculateMatchWinner([FromBody] MatchPredictionDto match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responseDto = await _teamsMatchWinnerManager.PredictMatchWinner(match);

            if (responseDto is null)
            {
                return UnprocessableEntity();
            }

            return Ok(responseDto);
        }
    }
}
