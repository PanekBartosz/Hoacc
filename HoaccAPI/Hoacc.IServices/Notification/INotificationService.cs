using HoaccIServices.Requests;

namespace HoaccIServices.Notification
{
    public interface INotificationService
    {
        Task<HoaccCommon.Notification.NotificationDTO> GetNotification(int notificationId);
        Task<HoaccCommon.Notification.NotificationDTO> CreateNotification(CreateNotification createNotification);
        Task EditNotification(int notificationId, EditNotification editNotification);
        Task RemoveNotification(int notificationId);
    }
}