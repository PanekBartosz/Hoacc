namespace HoaccCommon.Operations;

public class OperationsDTO
{
    public enum CategoryType
    {
        Bills,
        Food,
        Education,
        Entertainment
    }

    public OperationsDTO(int operationId, string type, DateTime date, string description,
        CategoryType category, double amount, int userId)
    {
        OperationId = operationId;
        Type = type;
        Date = date;
        Description = description;
        Category = category;
        Amount = amount;
        UserId = userId;
    }

    public OperationsDTO(string type, DateTime date, string description,
        CategoryType category, double amount, int userId)
    {
        Type = type;
        Date = date;
        Description = description;
        Category = category;
        Amount = amount;
        UserId = userId;
    }

    public int OperationId { get; set; }

    public string Type { get; set; } = "";

    public DateTime Date { get; set; }

    public string Description { get; set; } = "";

    public CategoryType Category { get; set; }

    public double Amount { get; set; }

    public int UserId { get; set; }

    public void EditOperations(string type, DateTime date, string description,
        CategoryType category, double amount)
    {
        Type = type;
        Date = date;
        Description = description;
        Category = category;
        Amount = amount;
    }
}