using HoaccAPI.ViewModels;

namespace HoaccAPI.Mappers
{
    public class UserToUserViewModelMapper
    {
        public static UserViewModel UserToUserViewModel(HoaccCommon.User.UserDTO user)
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