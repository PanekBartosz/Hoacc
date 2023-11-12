namespace HoaccCommon.User;

public class UserDTO
{
    public UserDTO(int userId, string email, string password)
    {
        UserId = userId;
        Email = email;
        Password = password;
    }

    public UserDTO(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public void UpdateUserPassword(string email, string password)
    {
        Email = email;
        Password = password;
    }
}