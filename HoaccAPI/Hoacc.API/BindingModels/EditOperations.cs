using System.ComponentModel.DataAnnotations;
using HoaccDomain.Operations;

namespace HoaccAPI.BindingModels
{
    public class EditOperations
    {
        [Required]
        [Display(Name = "OperationId")]
        public int OperationId { get; init; }

        [Required] 
        [Display(Name = "Type")] 
        public string? Type { get; init; }
        
        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; init; }

        [Required]
        [Display(Name = "Description")]
        public string? Description { get; init; }
        
        [Required]
        [Display(Name = "Category")]
        public Operations.CategoryType Category { get; init; }
        
        [Required]
        [Display(Name = "Amount")]
        public double Amount { get; init; }
        
        [Required]
        [Display(Name = "UserId")]
        public int UserId { get; init; }
    }
}