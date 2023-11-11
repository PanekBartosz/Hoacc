namespace HoaccCommon.Notification;

public class NotificationDTO
{
    public int NotificationId { get; set; }
    
    public string Name { get; set; } = "";
    
    public DateTime Date { get; set; }
    
    public double Amount { get; set; }
    
    public int UserId { get; set; }
    public NotificationDTO(int notificationId, string name, DateTime date, double amount, int userId )
    {
        NotificationId = notificationId;
        Name = name;
        Date = date;
        Amount = amount;
        UserId = userId;
    }
    public NotificationDTO(string name, DateTime date, double amount, int userId )
    {
        Name = name;
        Date = date;
        Amount = amount;
        UserId = userId;
    }
    public void EditNotification(string name, DateTime date, double amount)
    {
        Name = name;
        Date = date;
        Amount = amount;
    }
}