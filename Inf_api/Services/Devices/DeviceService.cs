using Inf_Data.Entities;
using Inf_Repository.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Inf_api.Services.Devices
{
    public class DeviceService:IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeviceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

       public async Task<List<Device>> GetUsersSmartDevices(int userId)
       {
            var result =  _unitOfWork.DeviceRepository.GetAll();

            result = result.Where(item=>item.AssignedToUserId == userId);

            return await result.ToListAsync();

       }
    }
}
