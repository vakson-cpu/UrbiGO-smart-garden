using Inf_Data;
using Inf_Repository.Repository.Generic;

namespace Inf_Repository.Repository.Notifications
{
    public class NotificationRepository : GenericRepository<Inf_Data.Entities.Notifications>, INotificationRepository
    {
        public NotificationRepository(InfDbContext context) : base(context)
        {
        }
    }
}
