namespace HoaccDataSql.DAO;

public class Goals
{
    public int GoalsId { get; set; }
    
    public string Name { get; set; } = "";
    
    public double GoalAmount { get; set; }
    
    public double CurrentAmount { get; set; }
        
    public virtual User User { get; set; }
}