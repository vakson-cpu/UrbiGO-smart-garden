using Inf_Data.Entities;

namespace Inf_api.Helpers
{
    public interface IPlantHelper
    {
        float AdjustRandomly(float baseValue, Random random);
        double AdjustRandomly(double baseValue, Random random);
        bool IsPlantHealthy(Plant plant);
        NotificationType CheckPlantHealth(Plant plant);
    }
}
