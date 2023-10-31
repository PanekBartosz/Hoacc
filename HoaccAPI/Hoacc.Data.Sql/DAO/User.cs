namespace HoaccDataSql.DAO;

public class User
{
    public User()
    {
        Goals = new List<Goals>();
        Notification = new List<Notification>();
        Operations = new List<Operations>();
    }
    
    public int UserId { get; set; }
    
    public string Email { get; set; } = "";

    public string Password { get; set; } = "";
    public virtual ICollection<Goals> Goals { get; set; }
    public virtual ICollection<Notification> Notification { get; set; }
    public virtual ICollection<Operations> Operations { get; set; }
}