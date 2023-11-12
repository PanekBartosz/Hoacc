namespace HoaccDataSql.DAO;

public class Notification
{
    public int NotificationId { get; set; }

    public string Name { get; set; } = "";

    public DateTime Date { get; set; }

    public double Amount { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
}