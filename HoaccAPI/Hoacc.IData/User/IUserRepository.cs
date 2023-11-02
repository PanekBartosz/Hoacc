namespace HoaccIData.User
{
    public interface IUserRepository
    {
        Task<HoaccDomain.User.User> GetUser(string email);
        Task<HoaccDomain.User.User> GetUser(int userId);
        Task<int> CreateUser(HoaccDomain.User.User user);
        Task UpdateUserPassword(int userId, HoaccDomain.User.User user);
    }
}