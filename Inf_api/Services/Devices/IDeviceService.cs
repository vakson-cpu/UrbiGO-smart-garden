using Inf_Data.Entities;

namespace Inf_api.Services.Devices
{
    public interface IDeviceService
    {
        Task<List<Device>> GetUsersSmartDevices(int userId);

        Task AssingPlantToDevice(int plantId, int deviceId);

        Task SeedDevicesForNewUserAsync(int userId, int count = 5);
    }

}
