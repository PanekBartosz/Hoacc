using System.ComponentModel.DataAnnotations;

namespace HoaccDomain.Operations
{
    public class Operations
    {
        public enum CategoryType
        {
            Bills,
            Food,
            Education,
            Entertainment
        }
        
        public int OperationId { get; set; }

        public string? Type { get; set; }
    
        public DateTime Date { get; set; }
    
        public string? Description { get; set; }
        
        public CategoryType Category { get; set; }
    
        public double Amount { get; set; }
        public int UserId { get; set; }

        public Operations(int operationId, string type, DateTime date, string description,
            CategoryType category, double amount, int userId )
        {
            OperationId = operationId;
            Type = type;
            Date = date;
            Description = description;
            Category = category;
            Amount = amount;
            UserId = userId;
        }
        public void EditOperations(int operationId, string type, DateTime date, string description,
            CategoryType category, double amount, int userId )
        {
            OperationId = operationId;
            Type = type;
            Date = date;
            Description = description;
            Category = category;
            Amount = amount;
            UserId = userId;
        }

    }
}