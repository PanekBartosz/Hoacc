namespace HoaccCommon.Notification;

public interface INotificationRepositoryDTO
{
    Task<NotificationDTO> GetNotification(int notificationId);
    Task<int> CreateNotification(NotificationDTO notification);
    Task EditNotification(int notificationId, NotificationDTO notification);
    Task RemoveNotification(int notificationId);
}