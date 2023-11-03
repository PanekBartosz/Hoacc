namespace HoaccIServices.Requests
{
    public class EditOperations
    {
        public enum CategoryType
        {
            Bills,
            Food,
            Education,
            Entertainment
        }
        public string Type { get; set; } = "";
    
        public DateTime Date { get; set; }
    
        public string Description { get; set; } = "";
        
        public CategoryType Category { get; set; }
    
        public double Amount { get; set; }
    }
}