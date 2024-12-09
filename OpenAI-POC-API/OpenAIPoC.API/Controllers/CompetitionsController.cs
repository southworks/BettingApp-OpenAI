using Microsoft.AspNetCore.Mvc;
using OpenAIPoC.API.Domain.Competitions;
using OpenAIPoC.API.Core.Competitions.Dtos;
using OpenAIPoC.API.Core.Competitions;

namespace OpenAIPoC.API.Controllers
{
    [Route("competitions")]
    [ApiController]
    public class CompetitionsController : ControllerBase
    {
        private readonly IRepository<Competition> _competitionsRepository;
        private readonly CompetitionCreateManager _competitionCreateManager;

        public CompetitionsController(IRepository<Competition> competitionsRepository, CompetitionCreateManager competitionCreateManager)
        {
            _competitionsRepository = competitionsRepository;
            _competitionCreateManager = competitionCreateManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompetitions()
        {
            var competitions = await _competitionsRepository.GetAllAsync();

            if (competitions == null || !competitions.Any())
            {
                return NotFound("No competitions found.");
            }

            return Ok(competitions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompetition([FromBody] CompetitionDto competitionDto)
        {
            var result = await _competitionCreateManager.CreateCompetitionAsync(competitionDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            if (result.Competition == null)
            {
                return Problem("An unexpected error occurred while creating the competition.");
            }

            return CreatedAtAction(nameof(GetCompetitions), new { id = result.Competition.Id }, result.Competition);
        }

    }
}
