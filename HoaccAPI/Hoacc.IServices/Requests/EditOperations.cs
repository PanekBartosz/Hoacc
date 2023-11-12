using HoaccCommon.Operations;

namespace HoaccIServices.Requests;

public class EditOperations
{
    public string Type { get; set; } = "";

    public DateTime Date { get; set; }

    public string Description { get; set; } = "";

    public OperationsDTO.CategoryType Category { get; set; }

    public double Amount { get; set; }
}