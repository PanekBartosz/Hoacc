namespace HoaccCommon.Goals;

public class GoalsDTO
{
    public GoalsDTO(int goalsId, string name, double goalAmount, double currentAmount, int userId)
    {
        GoalsId = goalsId;
        Name = name;
        GoalAmount = goalAmount;
        CurrentAmount = currentAmount;
        UserId = userId;
    }

    public GoalsDTO(string name, double goalAmount, double currentAmount, int userId)
    {
        Name = name;
        GoalAmount = goalAmount;
        CurrentAmount = currentAmount;
        UserId = userId;
    }

    public int GoalsId { get; set; }

    public string Name { get; set; } = "";

    public double GoalAmount { get; set; }

    public double CurrentAmount { get; set; }

    public int UserId { get; set; }

    public void EditGoals(string name, double goalAmount, double currentAmount)
    {
        Name = name;
        GoalAmount = goalAmount;
        CurrentAmount = currentAmount;
    }
}