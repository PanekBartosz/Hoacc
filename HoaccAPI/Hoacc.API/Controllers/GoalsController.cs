using HoaccAPI.Mappers;
using HoaccAPI.Validation;
using HoaccDataSql;
using HoaccIServices.Goals;
using HoaccIServices.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoaccAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoalsController : ControllerBase
{
    private readonly HoaccDbContext _context;
    private readonly IGoalsService _goalsService;

    public GoalsController(HoaccDbContext context, IGoalsService goalsService)
    {
        _context = context;
        _goalsService = goalsService;
    }

    [Authorize]
    [HttpGet("{goalsId:min(1)}", Name = "GetGoals")]
    public async Task<IActionResult> GetGoals(int goalsId)
    {
        var goals = await _goalsService.GetGoals(goalsId);
        if (goals != null) return Ok(GoalsToGoalsViewModelMapper.GoalsToGoalsViewModel(goals));
        return NotFound();
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetGoalsByUser(int userId)
    {
        // Extract user identity claim from the JWT token
        var userIdentityClaim = User.Claims.FirstOrDefault(c => c.Type == "UserIdentity")?.Value;

        // Ensure the user identity claim matches the requested user's identity
        if (userIdentityClaim != $"User:{userId}")
        {
            return Forbid(); // Return 403 Forbidden if the user is not authorized
        }
        
        var goal = await _goalsService.GetGoalsByUser(userId);

        if (goal == null || !goal.Any()) return NotFound($"No notification found for user ID {userId}");

        return Ok(goal);
    }

    [ValidateModel]
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateGoals createGoals)
    {
        var goals = await _goalsService.CreateGoals(createGoals);
        return Created(goals.GoalsId.ToString(), GoalsToGoalsViewModelMapper
            .GoalsToGoalsViewModel(goals));
    }

    [ValidateModel]
    [Authorize]
    [HttpPut("{goalsId:min(1)}", Name = "EditGoals")]
    public async Task<IActionResult> EditGoals([FromBody] EditGoals editGoals, int goalsId)
    {
        await _goalsService.EditGoals(goalsId, editGoals);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{goalsId:min(1)}", Name = "RemoveGoals")]
    public async Task<IActionResult> RemoveGoals(int goalsId)
    {
        await _goalsService.RemoveGoals(goalsId);
        return NoContent();
    }
}