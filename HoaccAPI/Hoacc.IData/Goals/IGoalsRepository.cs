namespace HoaccIData.Goals
{
    public interface IGoalsRepository
    {
        Task<HoaccDomain.Goals.Goals> GetGoals(int goalsId);
        Task<int> CreateGoals(HoaccDomain.Goals.Goals goals);
        Task EditGoals(int goalsId, HoaccDomain.Goals.Goals goals);
        Task RemoveGoals(int goalsId);
    }
}