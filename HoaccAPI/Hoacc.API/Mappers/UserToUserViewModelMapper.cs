using HoaccAPI.ViewModels;
using HoaccCommon.User;

namespace HoaccAPI.Mappers;

public class UserToUserViewModelMapper
{
    public static UserViewModel UserToUserViewModel(UserDTO user)
    {
        var userViewModel = new UserViewModel
        {
            UserId = user.UserId,
            Email = user.Email,
            Password = user.Password
        };
        return userViewModel;
    }
}