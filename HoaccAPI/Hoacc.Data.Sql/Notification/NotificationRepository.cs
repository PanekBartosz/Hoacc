using HoaccCommon.Notification;
using HoaccIData.Notification;
using HoaccIData.User;
using Microsoft.EntityFrameworkCore;

namespace HoaccDataSql.Notification
{
    public class NotificationRepository: INotificationRepositoryDTO
    {
        private readonly HoaccDbContext _context;
        private INotificationRepositoryDTO _notificationRepositoryImplementation;

        public NotificationRepository(HoaccDbContext context)
        {
            _context = context;
        }
        public async Task<NotificationDTO> GetNotification(int notificationId)
        {
            var notification = await _context.Notification.FirstOrDefaultAsync(x=>x.NotificationId == notificationId);
            return new NotificationDTO(notification.NotificationId,
                notification.Name,
                notification.Date,
                notification.Amount,
                notification.UserId);
        }
        
        public async Task<int> CreateNotification(NotificationDTO notification)
        {
            var notificationDAO =  new DAO.Notification { 
                NotificationId = notification.NotificationId,
                Name = notification.Name,
                Date = notification.Date,
                Amount = notification.Amount,
                UserId = notification.UserId
            };
            await _context.AddAsync(notificationDAO);
            await _context.SaveChangesAsync();
            return notificationDAO.NotificationId;
        }

        public async Task EditNotification(int notificationId, NotificationDTO notification)
        {
            var editNotification = await _context.Notification.FirstOrDefaultAsync(
                x=>x.NotificationId == notification.NotificationId);
            editNotification.Name = notification.Name;
            editNotification.Date = notification.Date;
            editNotification.Amount = notification.Amount;
            await _context.SaveChangesAsync();
        }
        
        public async Task RemoveNotification(int notificationId)
        {
            var notification = await _context.Notification.FirstOrDefaultAsync(
                x => x.NotificationId == notificationId);

            if (notification == null)
            {
                throw new Exception("Notification not found");
            }
            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();
        }

    }

}