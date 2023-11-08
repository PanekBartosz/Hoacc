using HoaccIData.User;
using HoaccIServices.Requests;
using HoaccIServices.User;

namespace HoaccServices.User
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<HoaccDomain.User.User> GetUserByEmail(string email)
        {
            return _userRepository.GetUser(email);
        }

        public Task<HoaccDomain.User.User> GetUserByUserId(int userId)
        {
            return _userRepository.GetUser(userId);
        }

        public async Task<HoaccDomain.User.User> CreateUser(CreateUser createUser)
        {
            var user = new HoaccDomain.User.User(createUser.Email, createUser.Password);
            user.UserId = await _userRepository.CreateUser(user);
            return user;
        }

        public async Task UpdateUserPassword(UpdateUserPassword updateUserPassword, int userId)
        {
            var user = await _userRepository.GetUser(userId);
            user.Password = updateUserPassword.Password;
            
            await _userRepository.EditClient(userId, user);
        }
    }
    
}