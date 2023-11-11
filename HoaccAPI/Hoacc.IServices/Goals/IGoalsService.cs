using HoaccIServices.Requests;

namespace HoaccIServices.Goals
{
    public interface IGoalsService
    {
        Task<HoaccCommon.Goals.GoalsDTO> GetGoals(int goalsId);
        Task<HoaccCommon.Goals.GoalsDTO> CreateGoals(CreateGoals createGoals);
        Task EditGoals(int goalsId, EditGoals editGoals);
        Task RemoveGoals(int goalsId);
    }
}