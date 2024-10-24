using Inf_Repository.Repository.Generic;

namespace Inf_Repository.Repository.Plant
{
    public interface IPlantRepository : IGenericRepository<Inf_Data.Entities.Plant>
    {
        Task<List<Inf_Data.Entities.Plant>> GetAllPlants();
    }
}
