using System.Threading.Tasks;
using Inf_api.Extensions;
using Inf_Data.Entities;
using Inf_Repository.Repository.UnitOfWork;
using Inf_Transfer.DTOS.PlantsDtos;
using Inf_Transfer.utils;

namespace Inf_api.Services.Plant
{
    public class PlantService : IPlantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponse<Inf_Data.Entities.Plant>> AddPlant(PlantsDtos addPlant)
        {
            var plant = new Inf_Data.Entities.Plant();

            // Assuming CopyProperties is an extension method or a method to copy DTO properties to the entity
            addPlant.CopyProperties(plant);

            await _unitOfWork.PlantRepository.Create(plant);
            await _unitOfWork.SaveChanges();

            return new CustomResponse<Inf_Data.Entities.Plant>(
                message: "Plant added successfully",
                succeeded: true,
                data: plant
            );
        }

        public async Task<CustomResponse<List<Inf_Data.Entities.Plant>>> GetAllPlants()
        {
            var result = await _unitOfWork.PlantRepository.GetAllPlants();
            var response = new CustomResponse<List<Inf_Data.Entities.Plant>>(
                message: "Request succeeded",
                succeeded: true,
                data: result
            );
            return response;
        }

    }
}
