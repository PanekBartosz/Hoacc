using HoaccCommon.User;
using HoaccIServices.Requests;
using HoaccIServices.User;

namespace HoaccServices.User;

public class UserService : IUserService
{
    private readonly IUserRepositoryDTO _userRepository;

    public UserService(IUserRepositoryDTO userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<UserDTO> GetUserByEmail(string email)
    {
        return _userRepository.GetUser(email);
    }

    public Task<UserDTO> GetUserByUserId(int userId)
    {
        return _userRepository.GetUser(userId);
    }
    
    public async Task<string?> GetEmailByUserId(int userId)
    {
        var user = await _userRepository.GetUser(userId);
        return user?.Email;
    }

    public async Task<UserDTO> CreateUser(CreateUser createUser)
    {
        var user = new UserDTO(createUser.Email, createUser.Password);
        user.UserId = await _userRepository.CreateUser(user);
        return user;
    }

    public async Task UpdateUserPassword(string hashedPassword, int userId)
    {
        var user = await _userRepository.GetUser(userId);
        user.Password = hashedPassword;

        await _userRepository.UpdateUserPassword(userId, user);
    }
}