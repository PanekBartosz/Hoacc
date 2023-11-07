namespace HoaccDomain.Notification
{
    public class Notification
    {
        public int NotificationId { get; set; }
    
        public string? Name { get; set; }
    
        public DateTime Date { get; set; }
    
        public double Amount { get; set; }
        public int UserId { get; set; }

        public Notification(int notificationId, string name, DateTime date, double amount, int userId )
        {
            NotificationId = notificationId;
            Name = name;
            Date = date;
            Amount = amount;
            UserId = userId;
        }
        public void EditNotification(int notificationId, string name, DateTime date, double amount, int userId )
        {
            NotificationId = notificationId;
            Name = name;
            Date = date;
            Amount = amount;
            UserId = userId;
        }

    }
}