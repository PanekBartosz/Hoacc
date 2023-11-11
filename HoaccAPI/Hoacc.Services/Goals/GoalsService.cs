using HoaccIData.Goals;
using HoaccIServices.Goals;
using HoaccIServices.Requests;

namespace HoaccServices.Goals
{
    public class GoalsService: IGoalsService
    {
        private readonly IGoalsRepository _goalsRepository;

        public GoalsService(IGoalsRepository goalsRepository)
        {
            _goalsRepository = goalsRepository;
        }

        public Task<HoaccCommon.Goals.GoalsDTO> GetGoals(int goalsId)
        {
            return _goalsRepository.GetGoals(goalsId);
        }

        public async Task<HoaccCommon.Goals.GoalsDTO> CreateGoals(CreateGoals createGoals)
        {
            var goals = new HoaccCommon.Goals.GoalsDTO(createGoals.Name,
                createGoals.GoalAmount, createGoals.CurrentAmount, createGoals.UserId);
            goals.GoalsId = await _goalsRepository.CreateGoals(goals);
            return goals;
        }

        public async Task EditGoals(int goalsId, EditGoals createGoals)
        {
            var goals = await _goalsRepository.GetGoals(goalsId);
            goals.EditGoals(createGoals.Name, createGoals.GoalAmount, createGoals.CurrentAmount);
            await _goalsRepository.EditGoals(goalsId, goals);
        }
        public async Task RemoveGoals(int goalsId)
        {
            await _goalsRepository.RemoveGoals(goalsId);
        }
    }
    
}