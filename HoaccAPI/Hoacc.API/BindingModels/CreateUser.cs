using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HoaccAPI.BindingModels
{
    public class CreateUser
    {
        [Required]
        [Display(Name = "UserId")]
        public int UserId { get; init; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; init; }
        
        [Required]
        [Display(Name = "Password")]
        public string Password { get; init; }
        
    }
}