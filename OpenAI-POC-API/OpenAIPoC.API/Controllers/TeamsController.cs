using Microsoft.AspNetCore.Mvc;
using OpenAIPoC.API.Core.Teams;
using OpenAIPoC.API.Core.Teams.Dtos;
using OpenAIPoC.API.Domain.Teams;

namespace OpenAIPoC.API.Controllers
{
    [Route("teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsManager _teamsManager;
        private readonly IRepository<Team> _teamsRepository;

        public TeamsController(ITeamsManager teamsManager, IRepository<Team> teamsRepository)
        {
            _teamsManager = teamsManager;
            _teamsRepository = teamsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _teamsRepository.GetAllAsync();

            if (teams == null || !teams.Any())
            {
                return NotFound("No teams found.");
            }

            return Ok(teams);
        }

        [HttpPost("lineups")]
        public async Task<IActionResult> Lineups([FromBody] MatchDto match)
        {
            var responseDto = await _teamsManager.PredictLineUpsAsync(match);

            if (responseDto is null)
            {
                return UnprocessableEntity();
            }

            return Ok(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam([FromBody] CreateTeamDto createTeamDto)
        {
            if (createTeamDto == null || string.IsNullOrEmpty(createTeamDto.Name) || createTeamDto.Squad == null || !createTeamDto.Squad.Any())
            {
                return BadRequest("Invalid team data.");
            }

            var existingTeam = (await _teamsRepository.GetAllAsync())
                .FirstOrDefault(t => t.Name == createTeamDto.Name);

            if (existingTeam != null)
            {
                return BadRequest("A team with the same name already exists in the database.");
            }

            var team = new Team
            {
                Name = createTeamDto.Name,
                Squad = createTeamDto.Squad.Select(p => new Player
                {
                    Name = p.Name,
                    Position = p.Position
                }).ToList()
            };

            await _teamsRepository.AddAsync(team);

            return CreatedAtAction(nameof(GetAll), new { id = team.Id }, team);
        }
    }
}
