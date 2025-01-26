using Inf_Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inf_Data.Entities
{
    public class Plant
    {
        public int Id { get; set; }

        public string LatinName { get; set; }
        public string LocalName { get; set; }

        public double CurrentWattering { get; set; }

        public float CurrentTemperature { get; set; }

        public float CurrentIllumination { get; set; }

        public bool IsHealthy { get; set; } = true; // This needs to be updated based on parameters
        public int PlantSpecificationId { get; set; }

        public string Code { get; set; } = String.Empty;

        public DateTime? BoughtAt { get; set; } //If its null the it means it hasnt been bought yet
        // For the simplicity cases we dont want to make it that way.

        public double Price { get; set; } = 100;

        [ForeignKey(nameof(PlantSpecificationId))]
        public PlantSpecifications PlantSpecification { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        public Device? Device { get; set; }

        //Health

        public PlantHealthEnum PlantHealth { get; set; } = PlantHealthEnum.HEALTHY;

    }
}
