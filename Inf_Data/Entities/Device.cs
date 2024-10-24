using System.ComponentModel.DataAnnotations.Schema;

namespace Inf_Data.Entities
{
    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int SerialNumber { get; set; }

        public int AssignedToUserId { get; set; }

        [ForeignKey(nameof(AssignedToUserId))]
        public AppUser? AssignedToUser { get; set; }

        public int? PlantToMonitorId { get; set; }

        public Plant? PlantToMonitor { get; set; }

    }
}
