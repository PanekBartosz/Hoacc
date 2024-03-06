using HoaccAPI.Mappers;
using HoaccAPI.Validation;
using HoaccDataSql;
using HoaccIServices.Notification;
using HoaccIServices.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoaccAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly HoaccDbContext _context;
    private readonly INotificationService _notificationService;

    public NotificationController(HoaccDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    [Authorize]
    [HttpGet("{notificationId:min(1)}", Name = "GetNotification")]
    public async Task<IActionResult> GetNotification(int notificationId)
    {
        var notification = await _notificationService.GetNotification(notificationId);
        if (notification != null)
            return Ok(NotificationToNotificationViewModelMapper.NotificationToNotificationViewModel(notification));
        return NotFound();
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetNotificationsByUser(int userId)
    {
        // Extract user identity claim from the JWT token
        var userIdentityClaim = User.Claims.FirstOrDefault(c => c.Type == "UserIdentity")?.Value;

        // Ensure the user identity claim matches the requested user's identity
        if (userIdentityClaim != $"User:{userId}")
        {
            return Forbid(); // Return 403 Forbidden if the user is not authorized
        }

        var notification = await _notificationService.GetNotificationByUser(userId);

        if (notification == null || !notification.Any()) return NotFound($"No notification found for user ID {userId}");

        return Ok(notification);
    }

    [ValidateModel]
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateNotification createNotification)
    {
        var notification = await _notificationService.CreateNotification(createNotification);
        return Created(notification.NotificationId.ToString(), NotificationToNotificationViewModelMapper
            .NotificationToNotificationViewModel(notification));
    }

    [Authorize]
    [ValidateModel]
    [HttpPut("{notificationId:min(1)}", Name = "EditNotification")]
    public async Task<IActionResult> EditNotification([FromBody] EditNotification editNotification, int notificationId)
    {
        await _notificationService.EditNotification(notificationId, editNotification);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{notificationId:min(1)}", Name = "RemoveNotification")]
    public async Task<IActionResult> RemoveNotification(int notificationId)
    {
        await _notificationService.RemoveNotification(notificationId);
        return NoContent();
    }
}