using Inf_Data;
using Inf_Repository.Repository.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inf_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly InfDbContext _context;
        private readonly INotificationRepository _notificationRepository;
        public NotificationsController(INotificationRepository notificationRepository,InfDbContext infDbContext)
        {
            _notificationRepository = notificationRepository;  
            _context = infDbContext;
        }
        [HttpGet("GetUserNotifications/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var notifications = await _context.Notifications
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            var shouldNotify = notifications.Any(x => !x.IsRead); // Assuming 'isRead' indicates if the notification is read

            var response = new
            {
                ShouldNotify = shouldNotify,
                Notifications = notifications
            };

            return Ok(response);
        }

        [HttpPut("MarkAsRead/{userId}")]
        public async Task<IActionResult> MarkAsRead(int userId)
        {
            var notifications = await _context.Notifications
                                    .Where(x => x.UserId == userId)
                                    .OrderByDescending(x => x.Date)
                                    .ToListAsync();
            foreach (var item in notifications)
            {
                item.IsRead = true;
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
