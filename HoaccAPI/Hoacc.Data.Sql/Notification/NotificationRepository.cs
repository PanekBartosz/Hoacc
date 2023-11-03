using HoaccIData.Notification;
using HoaccIData.User;
using Microsoft.EntityFrameworkCore;

namespace HoaccDataSql.Notification
{
    public class NotificationRepository: INotificationRepository
    {
        private readonly HoaccDbContext _context;
        private INotificationRepository _notificationRepositoryImplementation;

        public NotificationRepository(HoaccDbContext context)
        {
            _context = context;
        }
        public async Task<HoaccDomain.Notification.Notification> GetNotification(int notificationId)
        {
            var notification = await _context.Notification.FirstOrDefaultAsync(x=>x.NotificationId == notificationId);
            return new HoaccDomain.Notification.Notification(notification.NotificationId,
                notification.Name,
                notification.Date,
                notification.Amount,
                notification.UserId);
        }
        
        public async Task<int> CreateNotification(HoaccDomain.Notification.Notification notification)
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

        public async Task EditNotification(int notificationId, HoaccDomain.Notification.Notification notification)
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