namespace HoaccDomain.Goals;

public class Goals
{
    public Goals(int goalsId, string name, double goalAmount, double currentAmount, int userId)
    {
        GoalsId = goalsId;
        Name = name;
        GoalAmount = goalAmount;
        CurrentAmount = currentAmount;
        UserId = userId;
    }

    public int GoalsId { get; set; }

    public string? Name { get; set; }

    public double GoalAmount { get; set; }

    public double CurrentAmount { get; set; }

    public int UserId { get; set; }

    public void EditGoals(int goalsId, string name, double goalAmount, double currentAmount, int userId)
    {
        GoalsId = goalsId;
        Name = name;
        GoalAmount = goalAmount;
        CurrentAmount = currentAmount;
        UserId = userId;
    }
}