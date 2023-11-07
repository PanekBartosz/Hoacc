using HoaccIServices.Requests;

namespace HoaccIServices.User
{
    public interface IUserService
    {
        Task<HoaccCommon.User.UserDTO> GetUserByEmail(string email);
        Task<HoaccCommon.User.UserDTO> GetUserByUserId(int userId);
        Task<HoaccCommon.User.UserDTO> CreateUser(CreateUser createUser);
        Task UpdateUserPassword(UpdateUserPassword updateUserPassword, int userId);
    }
}