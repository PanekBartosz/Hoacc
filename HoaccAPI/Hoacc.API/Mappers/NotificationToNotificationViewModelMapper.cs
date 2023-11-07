using HoaccAPI.ViewModels;

namespace HoaccAPI.Mappers
{
    public class NotificationToNotificationViewModelMapper
    {
        public static NotificationViewModel 
            NotificationToNotificationViewModel(HoaccDomain.Notification.Notification notification)
        {
            var notificationViewModel = new NotificationViewModel
            {
                NotificationId = notification.NotificationId,
                Name = notification.Name,
                Date = notification.Date,
                Amount = notification.Amount,
                UserId = notification.UserId
            };
            return notificationViewModel;
        }

    }
}