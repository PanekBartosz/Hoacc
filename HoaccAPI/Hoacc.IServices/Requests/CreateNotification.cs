namespace HoaccIServices.Requests;

public class CreateNotification
{
    public int NotificationId { get; set; }

    public string Name { get; set; } = "";

    public DateTime Date { get; set; }

    public double Amount { get; set; }

    public int UserId { get; set; }
}