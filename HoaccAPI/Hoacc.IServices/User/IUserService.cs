using HoaccCommon.User;
using HoaccIServices.Requests;

namespace HoaccIServices.User;

public interface IUserService
{
    Task<UserDTO> GetUserByEmail(string email);
    Task<UserDTO> GetUserByUserId(int userId);
    Task<string?> GetEmailByUserId(int userId);
    Task<UserDTO> CreateUser(CreateUser createUser);
    Task UpdateUserPassword(UpdateUserPassword updateUserPassword, int userId);
}