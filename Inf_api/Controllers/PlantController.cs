using Inf_api.Services.Plant;
using Inf_api.Services.PlantSpecs;
using Inf_Repository.Repository.Plant;
using Inf_Repository.Repository.PlantSpecs;
using Inf_Repository.Repository.UnitOfWork;
using Inf_Transfer.DTOS.PlantsDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inf_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;

        private readonly IPlantSpecService _plantSpecService;
        public PlantController(IPlantService plantService, IPlantSpecService plantSpecService)
        {
            _plantService = plantService;
            _plantSpecService = plantSpecService;   
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

        public async Task<IActionResult> GetAllPlantSpecs(string? searchTerm, int page, int pageSize)
        {
            var result = await _plantSpecService.GetAllPlantSpecs(searchTerm,pageSize, page);
            return Ok(result);
        }

    }
}
