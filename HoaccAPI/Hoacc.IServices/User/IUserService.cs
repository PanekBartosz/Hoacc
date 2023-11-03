using HoaccIServices.Requests;

namespace HoaccIServices.User
{
    public interface IUserService
    {
        Task<HoaccDomain.User.User> GetUserByEmail(string email);
        Task<HoaccDomain.User.User> GetUserByUserId(int userId);
        Task<HoaccDomain.User.User> CreateUser(CreateUser createUser);
        Task UpdateUserPassword(UpdateUserPassword updateUserPassword, int userId);
    }
}