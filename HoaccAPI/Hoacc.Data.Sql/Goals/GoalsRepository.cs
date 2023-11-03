using HoaccIData.Goals;
using HoaccIData.User;
using Microsoft.EntityFrameworkCore;

namespace HoaccDataSql.Goals
{
    public class GoalsRepository: IGoalsRepository
    {
        private readonly HoaccDbContext _context;
        private IGoalsRepository _goalsRepositoryImplementation;

        public GoalsRepository(HoaccDbContext context)
        {
            _context = context;
        }
        public async Task<HoaccDomain.Goals.Goals> GetGoals(int goalsId)
        {
            var goals = await _context.Goals.FirstOrDefaultAsync(x=>x.GoalsId == goalsId);
            return new HoaccDomain.Goals.Goals(goals.GoalsId,
                goals.Name,
                goals.GoalAmount,
                goals.CurrentAmount,
                goals.UserId);
        }
        
        public async Task<int> CreateGoals(HoaccDomain.Goals.Goals goals)
        {
            var goalsDAO =  new DAO.Goals { 
                GoalsId = goals.GoalsId,
                Name = goals.Name,
                GoalAmount = goals.GoalAmount,
                CurrentAmount = goals.CurrentAmount,
                UserId = goals.UserId
            };
            await _context.AddAsync(goalsDAO);
            await _context.SaveChangesAsync();
            return goalsDAO.GoalsId;
        }

        public async Task EditGoals(int goalsId, HoaccDomain.Goals.Goals goals)
        {
            var editGoals = await _context.Goals.FirstOrDefaultAsync(
                x=>x.GoalsId == goals.GoalsId);
            editGoals.Name = goals.Name;
            editGoals.GoalAmount = goals.GoalAmount;
            editGoals.CurrentAmount = goals.CurrentAmount;
            await _context.SaveChangesAsync();
        }
        
        public async Task RemoveGoals(int goalsId)
        {
            var goals = await _context.Goals.FirstOrDefaultAsync(
                x => x.GoalsId == goalsId);

            if (goals == null)
            {
                throw new Exception("Goal not found");
            }
            _context.Goals.Remove(goals);
            await _context.SaveChangesAsync();
        }

    }

}