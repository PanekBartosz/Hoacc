namespace HoaccCommon.Goals;

public class GoalsDTO
{
    public int GoalsId { get; set; }
    
    public string Name { get; set; } = "";
    
    public double GoalAmount { get; set; }
    
    public double CurrentAmount { get; set; }
    
    public int UserId { get; set; }
}