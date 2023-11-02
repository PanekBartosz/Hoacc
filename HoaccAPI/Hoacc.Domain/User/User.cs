namespace HoaccDomain.User
{
    public class User
    {
        public int UserId { get; set; }
    
        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

        public User(int userId, string email, string password)
        {
            UserId = userId;
            Email = email;
            Password = password;
        }
        public void UpdateUserPassword(int userId, string email, string password)
        {
            UserId = userId;
            Email = email;
            Password = password;
        }

    }
}