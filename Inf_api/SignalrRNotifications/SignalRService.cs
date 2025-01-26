using Microsoft.AspNetCore.SignalR;
using Inf_Data.Entities;

namespace Inf_api.SignalrRNotifications
{
    public class SignalRService : ISignalRService
    {
        private readonly IHubContext<NotificationsHub> _hubContext;

        public SignalRService(IHubContext<NotificationsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyUser(Notifications notification)
        {
            await _hubContext.Clients.Group($"User_{notification.UserId}")
                .SendAsync("ReceiveNotification", notification);
        }
    }
}
