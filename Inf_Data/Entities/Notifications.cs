using Inf_Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inf_Data.Entities
{
    public class Notifications
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public bool IsRead { get; set; } = false;
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        public NotificaitonType NotificaitonType { get; set; }
    }
}
