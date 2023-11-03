﻿namespace HoaccIServices.Requests;
public class CreateOperations
{
    public enum CategoryType
    {
        Bills,
        Food,
        Education,
        Entertainment
    }
        
    public int OperationId { get; set; }

    public string Type { get; set; } = "";
    
    public DateTime Date { get; set; }
    
    public string Description { get; set; } = "";
        
    public CategoryType Category { get; set; }
    
    public double Amount { get; set; }
    
    public int UserId { get; set; }
}