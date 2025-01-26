using Inf_api.Services.Devices;
using Microsoft.AspNetCore.Mvc;

namespace Inf_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController :ControllerBase
    {
        private readonly  IDeviceService _deviceService;    
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet("{userId}")]   
        public async Task<IActionResult> GetUsersDevices(int userId)
        {
            var result = await _deviceService.GetUsersSmartDevices(userId);
            return Ok(result);
                
        }

        [HttpPut("AssignDevice")]
        public async Task<IActionResult> AssignPlantToDevice(int plantId,int deviceId)
        {
            try
            {
                await _deviceService.AssingPlantToDevice(plantId, deviceId);
                return Ok("Device Assigned Correctly");
            }
            catch (Exception err)
            {
                throw;
            }
        }
    }
}
