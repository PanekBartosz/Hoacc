using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoaccAPI.ViewModels;

public class GoalsViewModel
{
    [Key] 
    public int GoalsId { get; set; }
    
    [Column(TypeName = "nvarchar(30)")]
    public string? Name { get; set; }
    
    public double GoalAmount { get; set; }
    
    public double CurrentAmount { get; set; }
    
    public int UserId { get; set; }
}