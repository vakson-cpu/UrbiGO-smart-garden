using Inf_api.Helpers.Enums;
using Inf_Data.Entities;

namespace Inf_api.Helpers
{
    public class PlantHelper:IPlantHelper
    {
        public PlantHelper()
        {
            
        }

        // Helper method to adjust a value randomly within the range of -10 to +5
        public  double AdjustRandomly(double baseValue, Random random)
        {
            double min = baseValue - 10;
            double max = baseValue + 5;
            return random.NextDouble() * (max - min) + min;
        }

        public  float AdjustRandomly(float baseValue, Random random)
        {
            float min = baseValue - 10;
            float max = baseValue + 5;
            return (float)(random.NextDouble() * (max - min) + min);
        }

        // Helper method to determine if the plant is healthy
        public  bool IsPlantHealthy(Plant plant)
        {
            if (plant.CurrentWattering < (double)plant.PlantSpecification.Wattering - 5 ||
                plant.CurrentTemperature < (float)plant.PlantSpecification.Temperature - 5 ||
                plant.CurrentIllumination < (float)plant.PlantSpecification.Illumination - 5)
            {
                return false; // Plant is unhealthy if any parameter is below the threshold
            }

            return true; // Plant is healthy otherwise
        }
        public NotificationType CheckPlantHealth(Plant plant)
        {
            var spec = plant.PlantSpecification;
            int warnings = 0;

            // Check if stats are below -5 or above +15 of the specification
            if (plant.CurrentWattering < (double)spec.Wattering - 5 || plant.CurrentWattering > (double)spec.Wattering + 15) warnings++;
            if (plant.CurrentTemperature < (double)spec.Temperature - 5 || plant.CurrentTemperature > (double)spec.Temperature + 15) warnings++;
            if (plant.CurrentIllumination < (double)spec.Illumination - 5 || plant.CurrentIllumination > (double)spec.Illumination + 15) warnings++;

            if (warnings >= 2)
            {
                plant.PlantHealth = (Inf_Data.Enums.PlantHealthEnum)PlantHealthEnum.DEAD;
                return NotificationType.Error; // Plant is dead
            }
            else if (warnings == 1)
            {
                plant.PlantHealth = (Inf_Data.Enums.PlantHealthEnum)PlantHealthEnum.NOT_HEALTHY;
                return NotificationType.Warning; // Plant is not healthy
            }

            plant.PlantHealth = (Inf_Data.Enums.PlantHealthEnum)PlantHealthEnum.HEALTHY;
            return NotificationType.Normal; // Plant is healthy
        }

    }
}
