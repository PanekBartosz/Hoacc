using System.ComponentModel.DataAnnotations;

namespace HoaccAPI.ViewModels;

public class Operations
{
    public enum CategoryType
    {
        Bills,
        Food,
        Education,
        Entertainment
    }
    
    [Key] 
    public int OperationId { get; set; }

    public string Type { get; set; } = "";
    
    public DateTime Date { get; set; }
    
    public string Description { get; set; } = "";
    
    public CategoryType Category { get; set; }
    
    public double Amount { get; set; }
}