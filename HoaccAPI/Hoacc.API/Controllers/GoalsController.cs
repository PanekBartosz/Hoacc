using HoaccAPI.Validation;
using HoaccDataSql;
using Microsoft.AspNetCore.Mvc;
using HoaccAPI.Mappers;
using HoaccIServices.Goals;
using HoaccIServices.Notification;

namespace HoaccAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly HoaccDbContext _context;
        private readonly IGoalsService _goalsService;

        public GoalsController(HoaccDbContext context,IGoalsService goalsService)
        {
            _context = context;
            _goalsService = goalsService;
        }

        [HttpGet("{goalsId:min(1)}", Name = "GetGoals")]
        public async Task<IActionResult> GetGoals(int goalsId)
        {
            var goals = await _goalsService.GetGoals(goalsId);
            if (goals != null)
            {
                return Ok(GoalsToGoalsViewModelMapper.GoalsToGoalsViewModel(goals));
            }
            return NotFound();
        }

        [ValidateModel]
        public async Task<IActionResult> Post([FromBody] HoaccIServices.Requests.CreateGoals createGoals)
        {
            var goals = await _goalsService.CreateGoals(createGoals);
            return Created(goals.GoalsId.ToString(),GoalsToGoalsViewModelMapper
                .GoalsToGoalsViewModel(goals)) ;
        }

        [ValidateModel]
        [HttpPut("{goalsId:min(1)}", Name = "EditGoals")]
        public async Task<IActionResult> EditGoals([FromBody] HoaccIServices.
            Requests.EditGoals editGoals, int goalsId)
        {
            await _goalsService.EditGoals(goalsId, editGoals);
            return NoContent();
        }
        
        [HttpDelete("{goalsId:min(1)}", Name = "RemoveGoals")]
        public async Task<IActionResult> RemoveGoals(int goalsId)
        {
            await _goalsService.RemoveGoals(goalsId);
            return NoContent();
        }

    }
}
