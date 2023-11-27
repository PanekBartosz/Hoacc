using System.ComponentModel.DataAnnotations;

namespace HoaccAPI.ViewModels;

public class OperationsViewModel
{
    public enum CategoryTypeViewModel
    {
        Other,
        Bills,
        Food,
        Education,
        Entertainment,
        Unknown
    }

    [Key] public int OperationId { get; set; }

    public string? Type { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public CategoryTypeViewModel Category { get; set; }

    public double Amount { get; set; }

    public int UserId { get; set; }
}