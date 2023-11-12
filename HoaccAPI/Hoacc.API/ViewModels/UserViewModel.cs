using System.ComponentModel.DataAnnotations;

namespace HoaccAPI.ViewModels;

public class UserViewModel
{
    [Key] public int UserId { get; set; }

    [Required] public string? Email { get; set; }

    [Required] public string? Password { get; set; }
}