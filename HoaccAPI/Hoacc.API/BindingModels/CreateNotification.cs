using System.ComponentModel.DataAnnotations;

namespace HoaccAPI.BindingModels
{
    public class CreateNotification
    {
        [Required]
        [Display(Name = "NotificationId")]
        public int NotificationId { get; init; }
        
        [Required]
        [Display(Name = "Name")]
        public string Name { get; init; }
        
        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; init; }
        
        [Required]
        [Display(Name = "Amount")]
        public double Amount { get; init; }
        
        [Required]
        [Display(Name = "UserId")]
        public int UserId { get; init; }
    }
}