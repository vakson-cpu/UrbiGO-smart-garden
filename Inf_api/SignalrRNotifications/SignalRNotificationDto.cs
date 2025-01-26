using Inf_Data.Entities;

namespace Inf_api.SignalrRNotifications
{
    public class SignalRNotificationDto
    {
        public int UserId { get; set; }

        public Notifications Notification { get; set; }

    }
}
