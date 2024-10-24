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

        public int PlantSpecificationId { get; set; }

        [ForeignKey(nameof(PlantSpecificationId))]
        public PlantSpecifications PlantSpecification { get; set; }



    }
}
