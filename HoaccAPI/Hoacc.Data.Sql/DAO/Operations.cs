

using System.ComponentModel.DataAnnotations;
using HoaccCommon.Operations;

namespace HoaccDataSql.DAO;

public class Operations
{
    [Key]
    public int OperationId { get; set; }

    public string Type { get; set; } = "";
    
    public DateTime Date { get; set; }
    
    public string Description { get; set; } = "";
    
    public OperationsDTO.CategoryType Category { get; set; }
    
    public double Amount { get; set; }
    public int UserId { get; set; }
    
    public virtual User User { get; set; }
}