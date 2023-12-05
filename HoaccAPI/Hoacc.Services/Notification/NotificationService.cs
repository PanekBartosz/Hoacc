using HoaccCommon.Notification;
using HoaccIServices.Notification;
using HoaccIServices.Requests;

namespace HoaccServices.Notification;

public class NotificationService : INotificationService
{
    private readonly INotificationRepositoryDTO _notificationRepository;

    public NotificationService(INotificationRepositoryDTO notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public Task<NotificationDTO> GetNotification(int notificationId)
    {
        return _notificationRepository.GetNotification(notificationId);
    }

    public async Task<IEnumerable<NotificationDTO>> GetNotificationByUser(int userId)
    {
        var notification = await _notificationRepository.GetNotificationByUser(userId);
        return notification;
    }

    public async Task<NotificationDTO> CreateNotification(CreateNotification createNotification)
    {
        var notification = new NotificationDTO(createNotification.Name,
            createNotification.Date, createNotification.Amount, createNotification.UserId);
        notification.NotificationId = await _notificationRepository.CreateNotification(notification);
        return notification;
    }

    public async Task EditNotification(int notificationId, EditNotification createNotification)
    {
        var notification = await _notificationRepository.GetNotification(notificationId);
        notification.EditNotification(createNotification.Name, createNotification.Date, createNotification.Amount);
        await _notificationRepository.EditNotification(notificationId, notification);
    }

    public async Task RemoveNotification(int notificationId)
    {
        await _notificationRepository.RemoveNotification(notificationId);
    }
}