using HoaccCommon.Operations;

namespace HoaccIServices.Requests;
public class CreateOperations
{

    public int OperationId { get; set; }

    public string? Type { get; set; }
    
    public DateTime Date { get; set; }
    
    public string? Description { get; set; }
        
    public OperationsDTO.CategoryType Category { get; set; }
    
    public double Amount { get; set; }
    
    public int UserId { get; set; }
}