namespace HoaccIData.Notification
{
    public interface INotificationRepository
    {
        Task<HoaccDomain.Notification.Notification> GetNotification(int notificationId);
        Task<int> CreateNotification(HoaccDomain.Notification.Notification notification);
        Task EditNotification(int notificationId, HoaccDomain.Notification.Notification notification);
        Task RemoveNotification(int notificationId);
    }
}