using Inf_Data.Entities;
using Inf_Transfer.utils;

namespace Inf_api.Services.PlantSpecs
{
    public interface IPlantSpecService
    {
        Task<QueryResponse<PlantSpecifications>> GetAllPlantSpecs(string search,int pageSize=10, int page = 1);
            
    
    
    }
}
