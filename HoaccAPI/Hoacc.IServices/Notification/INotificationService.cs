using HoaccCommon.Notification;
using HoaccIServices.Requests;

namespace HoaccIServices.Notification;

public interface INotificationService
{
    Task<NotificationDTO> GetNotification(int notificationId);
    Task<IEnumerable<NotificationDTO>> GetNotificationByUser(int userId);
    Task<NotificationDTO> CreateNotification(CreateNotification createNotification);
    Task EditNotification(int notificationId, EditNotification editNotification);
    Task RemoveNotification(int notificationId);
}