using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inf_Data.Entities
{
    public class PlantSpecifications
    {
        public int Id { get; set; }              // Represents the unique identifier for each plant specification
        public decimal Wattering { get; set; }    // Represents the water requirement of the plant
        public decimal Temperature { get; set; }  // Represents the ideal temperature for the plant
        public decimal Illumination { get; set; } // Represents the light requirements of the plant
        public string SpecieName { get; set; }    // Represents the name of the plant species
        public int Count { get; set; } = 5;

        public List<Plant> Plants { get; set; }
    }
}
