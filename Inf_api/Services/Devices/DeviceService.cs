using Inf_Data.Entities;
using Inf_Repository.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Inf_api.Services.Devices
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeviceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        public async Task AssingPlantToDevice(int plantId, int deviceId)
        {
            var device = await _unitOfWork.DeviceRepository.GetAllWithTracking().Where(x=>x.Id == deviceId).FirstOrDefaultAsync();
            if (device is null)
                return;
            device.PlantToMonitorId = plantId;
            await _unitOfWork.SaveChanges();
        }

        public async Task<List<Device>> GetUsersSmartDevices(int userId)
       {
            var result =  _unitOfWork.DeviceRepository.GetAll();

            result = result.Where(item=>item.AssignedToUserId == userId).Include(device=>device.PlantToMonitor);

            return await result.ToListAsync();

       }
    }
}
