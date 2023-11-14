namespace HoaccCommon.Goals;

public interface IGoalsRepositoryDTO
{
    Task<GoalsDTO> GetGoals(int goalsId);
    Task<IEnumerable<GoalsDTO>> GetGoalsByUser(int userId);
    Task<int> CreateGoals(GoalsDTO goals);
    Task EditGoals(int goalsId, GoalsDTO goals);
    Task RemoveGoals(int goalsId);
}