using HoaccCommon.User;

namespace HoaccIData.User;

public interface IUserRepository
{
    Task<UserDTO> GetUser(string email);
    Task<UserDTO> GetUser(int userId);
    Task<int> CreateUser(UserDTO user);
    Task UpdateUserPassword(int userId, UserDTO user);
}