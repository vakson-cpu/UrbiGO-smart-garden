using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inf_Transfer.DTOS.PlantsDtos
{
    public class PlantsDtos
    {
        public string LatinName { get; set; }
        public string LocalName { get; set; }

        public double CurrentWattering { get; set; }

        public float CurrentTemperature { get; set; }

        public float CurrentIllumination { get; set; }

        public int PlantSpecificationId { get; set; }

    }
}
