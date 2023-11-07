using System.ComponentModel.DataAnnotations;

namespace HoaccAPI.BindingModels
{
    public class CreateUser
    {
        [Required]
        [Display(Name = "UserId")]
        public int UserId { get; init; }
        
        [Required]
        [Display(Name = "Email")]
        public string Email { get; init; }
        
        [Required]
        [Display(Name = "Password")]
        public string Password { get; init; }
        
    }
}