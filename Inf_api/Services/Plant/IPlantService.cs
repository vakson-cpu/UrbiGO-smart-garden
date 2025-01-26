using Inf_Data.Entities;
using Inf_Repository.Repository.Generic;
using Inf_Transfer.DTOS.PlantsDtos;
using Inf_Transfer.utils;

namespace Inf_api.Services.Plant
{
    public interface IPlantService
    {
        Task<CustomResponse<Inf_Data.Entities.Plant>> AddPlant(PlantsDtos addPlant);
        Task<CustomResponse<List<Inf_Data.Entities.Plant>>> GetAllPlants();

        Task<CustomResponse<Inf_Data.Entities.Plant>> BuyPlant(int userId, int plantSpecId);

        Task<CustomResponse<List<Inf_Data.Entities.Plant>>> GetUsersPlants(int userId);
    }
}
