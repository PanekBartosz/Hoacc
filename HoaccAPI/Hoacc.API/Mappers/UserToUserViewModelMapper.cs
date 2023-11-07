using HoaccAPI.ViewModels;

namespace HoaccAPI.Mappers
{
    public class UserToUserViewModelMapper
    {
        public static UserViewModel UserToUserViewModel(HoaccDomain.User.User user)
        {
            var userViewModel = new UserViewModel
            {
                UserId = user.UserId,
                Email = user.Email,
                Password = user.Password,
            };
            return userViewModel;
        }

    }
}