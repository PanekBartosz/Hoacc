using HoaccCommon.Notification;
using HoaccIServices.Notification;
using HoaccIServices.Requests;

namespace HoaccServices.Notification
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationRepositoryDTO _notificationRepository;

        public NotificationService(INotificationRepositoryDTO notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public Task<HoaccCommon.Notification.NotificationDTO> GetNotification(int notificationId)
        {
            return _notificationRepository.GetNotification(notificationId);
        }

        public async Task<HoaccCommon.Notification.NotificationDTO> CreateNotification(CreateNotification createNotification)
        {
            var notification = new HoaccCommon.Notification.NotificationDTO(createNotification.Name,
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
    
}