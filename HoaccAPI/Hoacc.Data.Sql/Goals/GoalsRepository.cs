using HoaccCommon.Goals;
using Microsoft.EntityFrameworkCore;

namespace HoaccDataSql.Goals;

public class GoalsRepository : IGoalsRepositoryDTO
{
    private readonly HoaccDbContext _context;
    private IGoalsRepositoryDTO _goalsRepositoryImplementation;

    public GoalsRepository(HoaccDbContext context)
    {
        _context = context;
    }

    public async Task<GoalsDTO> GetGoals(int goalsId)
    {
        var goals = await _context.Goals.FirstOrDefaultAsync(x => x.GoalsId == goalsId);
        return new GoalsDTO(goals.GoalsId,
            goals.Name,
            goals.GoalAmount,
            goals.CurrentAmount,
            goals.UserId);
    }
    
    public async Task<IEnumerable<GoalsDTO>> GetGoalsByUser(int userId)
    {
        var goal = await _context.Goals
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return goal.Select(MapGoalsToDTO);
    }

    public async Task<int> CreateGoals(GoalsDTO goals)
    {
        var goalsDAO = new DAO.Goals
        {
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

    public async Task EditGoals(int goalsId, GoalsDTO goals)
    {
        var editGoals = await _context.Goals.FirstOrDefaultAsync(
            x => x.GoalsId == goals.GoalsId);
        editGoals.Name = goals.Name;
        editGoals.GoalAmount = goals.GoalAmount;
        editGoals.CurrentAmount = goals.CurrentAmount;
        await _context.SaveChangesAsync();
    }

    public async Task RemoveGoals(int goalsId)
    {
        var goals = await _context.Goals.FirstOrDefaultAsync(
            x => x.GoalsId == goalsId);

        if (goals == null) throw new Exception("Goal not found");
        _context.Goals.Remove(goals);
        await _context.SaveChangesAsync();
    }
    
    private GoalsDTO MapGoalsToDTO(DAO.Goals goals)
    {
        return new GoalsDTO(
            goals.GoalsId,
            goals.Name,
            goals.GoalAmount,
            goals.CurrentAmount,
            goals.UserId
        );
    }
}