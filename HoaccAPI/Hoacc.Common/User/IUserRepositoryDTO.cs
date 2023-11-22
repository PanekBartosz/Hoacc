using HoaccCommon.User;

public interface IUserRepositoryDTO
{
    Task<UserDTO> GetUser(string email);
    Task<UserDTO> GetUser(int userId);
    Task<string?> GetUserEmail(int userId);
    Task<int> CreateUser(UserDTO user);
    Task UpdateUserPassword(int userId, UserDTO user);
}