namespace HoaccIServices.Requests
{
    public class EditGoals
    {
        public string Name { get; set; } = "";
    
        public double GoalAmount { get; set; }
    
        public double CurrentAmount { get; set; }
    }
}