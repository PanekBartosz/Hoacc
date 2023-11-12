using System.ComponentModel.DataAnnotations;

namespace HoaccAPI.BindingModels;

public class CreateGoals
{
    [Required] [Display(Name = "GoalsId")] public int GoalsId { get; init; }

    [Required] [Display(Name = "Name")] public string Name { get; init; }

    [Required]
    [Display(Name = "GoalAmount")]
    public double GoalAmount { get; init; }

    [Required]
    [Display(Name = "CurrentAmount")]
    public double CurrentAmount { get; init; }

    [Required] [Display(Name = "UserId")] public int UserId { get; init; }
}