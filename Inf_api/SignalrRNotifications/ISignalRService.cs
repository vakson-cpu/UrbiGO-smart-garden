using Inf_Data.Entities;

namespace Inf_api.SignalrRNotifications
{
    public interface ISignalRService
    {
        Task NotifyUser(Notifications notification);
    }
}
