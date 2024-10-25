using Inf_Repository.Repository.Devices;
using Inf_Repository.Repository.Generic;
using Inf_Repository.Repository.Plant;
using Inf_Repository.Repository.User;

namespace Inf_Repository.Repository.UnitOfWork;
public interface IUnitOfWork:IAsyncDisposable
{
    IUserRepository UserRepository { get;set; }   
    IPlantRepository PlantRepository { get; set; }
    IDeviceRepository DeviceRepository { get; set; }
    Task SaveChanges();   
}