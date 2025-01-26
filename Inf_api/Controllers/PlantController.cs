using Inf_api.Services.Plant;
using Inf_api.Services.PlantSpecs;
using Inf_Repository.Repository.Plant;
using Inf_Transfer.DTOS.PlantsDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inf_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;

        private readonly IPlantSpecService _plantSpecService;

        private readonly IPlantRepository _plantRepository;
        public PlantController(IPlantService plantService, IPlantSpecService plantSpecService,IPlantRepository plantRepository)
        {
            _plantService = plantService;
            _plantSpecService = plantSpecService;
            _plantRepository = plantRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlants()
        {
            var result = await _plantService.GetAllPlants();    
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlant(PlantsDtos newPlant)
        {
            var result = await _plantService.AddPlant(newPlant);
            return Ok(result);
        }

        [HttpGet("GetAllSpecifications")]

        public async Task<IActionResult> GetAllPlantSpecs(string? searchTerm, int page=1, int pageSize = 10)
        {
            var result = await _plantSpecService.GetAllPlantSpecs(searchTerm,pageSize, page);
            return Ok(result);
        }
        [HttpPost("BuyPlant/{specieId}")]

        public async Task<IActionResult> BuyPlant(int userId,int specieId)
        {
            var result = await _plantService.BuyPlant(userId, specieId);

            return Ok(result);
        }
        [HttpGet("GetAllUsersPlants/{userId}")]
        public async Task<IActionResult> GetAllUsersPlants(int userId)
        {
            var result = await _plantService.GetUsersPlants(userId);

            return Ok(result);  
        }
        /// <summary>
        /// Endpoint for fetching plant detais
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a plant</returns>
        [HttpGet("GetPlantById/{id}")]
        
        public async Task<IActionResult> GetPlantById(int id)
        {
            var result = await _plantRepository.GetAllWithTracking().Where(x => x.Id == id).Include(x => x.PlantSpecification).FirstOrDefaultAsync();
            return Ok(result);
        }
    }
}
