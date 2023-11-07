using HoaccIServices.Requests;

namespace HoaccIServices.Notification
{
    public interface INotificationService
    {
        Task<HoaccCommon.Notification.NotificationDTO> GetNotification(int notificationId);
        Task<int> CreateNotification(CreateNotification createNotification);
        Task EditNotification(int notificationId, EditNotification editNotification);
        Task RemoveNotification(int notificationId);
    }
}