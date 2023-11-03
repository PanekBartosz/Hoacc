namespace HoaccIServices.Requests
{
    public class EditGoals
    {
        public int GoalsId { get; set; }
    
        public string Name { get; set; } = "";
    
        public double GoalAmount { get; set; }
    
        public double CurrentAmount { get; set; }
    
        public int UserId { get; set; }
    }
}