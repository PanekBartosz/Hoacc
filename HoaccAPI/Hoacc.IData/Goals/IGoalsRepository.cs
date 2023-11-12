using HoaccCommon.Goals;

namespace HoaccIData.Goals
{
    public interface IGoalsRepository
    {
        Task<GoalsDTO> GetGoals(int goalsId);
        Task<int> CreateGoals(GoalsDTO goals);
        Task EditGoals(int goalsId, GoalsDTO goals);
        Task RemoveGoals(int goalsId);
    }
}