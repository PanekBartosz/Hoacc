using HoaccAPI.Validation;
using HoaccDataSql;
using Microsoft.AspNetCore.Mvc;
using HoaccAPI.Mappers;
using HoaccIServices.Notification;

namespace HoaccAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly HoaccDbContext _context;
        private readonly INotificationService _notificationService;

        public NotificationController(HoaccDbContext context,INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        [HttpGet("{notificationId:min(1)}", Name = "GetNotification")]
        public async Task<IActionResult> GetNotification(int notificationId)
        {
            var notification = await _notificationService.GetNotification(notificationId);
            if (notification != null)
            {
                return Ok(NotificationToNotificationViewModelMapper.NotificationToNotificationViewModel(notification));
            }
            return NotFound();
        }

        [ValidateModel]
        public async Task<IActionResult> Post([FromBody] HoaccIServices.Requests.CreateNotification createNotification)
        {
            var notification = await _notificationService.CreateNotification(createNotification);
            return Created(notification.NotificationId.ToString(),NotificationToNotificationViewModelMapper
                .NotificationToNotificationViewModel(notification)) ;
        }

        [ValidateModel]
        [HttpPut("{notificationId:min(1)}", Name = "EditNotification")]
        public async Task<IActionResult> EditNotification([FromBody] HoaccIServices.
            Requests.EditNotification editNotification, int notificationId)
        {
            await _notificationService.EditNotification(notificationId, editNotification);
            return NoContent();
        }
        
        [HttpDelete("{notificationId:min(1)}", Name = "RemoveNotification")]
        public async Task<IActionResult> RemoveNotification(int notificationId)
        {
            await _notificationService.RemoveNotification(notificationId);
            return NoContent();
        }

    }
}
