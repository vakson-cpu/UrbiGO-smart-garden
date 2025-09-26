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

        public async Task SeedDevicesForNewUserAsync(int userId, int count = 5)
        {
            // Idempotency: if user already has devices, skip
            var alreadyHas = await _unitOfWork.DeviceRepository
                .GetAll()
                .AnyAsync(d => d.AssignedToUserId == userId);

            if (alreadyHas) return;

            var devices = Enumerable.Range(1, count).Select(i => new Device
            {
                Name = $"Device {i}",
                SerialNumber = GenerateSerial(),      // make it unique-ish
                AssignedToUserId = userId,
                PlantToMonitorId = null
            }).ToList();

            // If your repo has AddRangeAsync, use it. Otherwise loop .Add
            foreach (var d in devices)
                await _unitOfWork.DeviceRepository.Create(d);

        }
        private static int GenerateSerial()
        {
            Random _rng = new Random();
            // Range 100000–999999 (inclusive), always 6 digits
            return _rng.Next(100000, 1000000);
        }
    }
}
