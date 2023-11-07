using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoaccAPI.ViewModels;

public class NotificationViewModel
{
    [Key] 
    public int NotificationId { get; set; }
    
    [Column(TypeName = "nvarchar(20)")]
    public string? Name { get; set; }
    
    public DateTime Date { get; set; }
    
    public double Amount { get; set; }
    
    public int UserId { get; set; }
}