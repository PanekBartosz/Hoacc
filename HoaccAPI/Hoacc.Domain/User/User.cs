namespace HoaccDomain.User
{
    public class User
    {
        public int UserId { get; set; }
    
        public string? Email { get; set; }

        public string? Password { get; set; }

        public User(int userId, string email, string password)
        {
            UserId = userId;
            Email = email;
            Password = password;
        }
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public void UpdateUserPassword(string email, string password)
        {
            Email = email;
            Password = password;
        }

    }
}