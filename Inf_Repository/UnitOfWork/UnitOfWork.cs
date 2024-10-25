using Inf_Data;
using Inf_Repository.Repository.Devices;
using Inf_Repository.Repository.Plant;
using Inf_Repository.Repository.PlantSpecs;
using Inf_Repository.Repository.User;

namespace Inf_Repository.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public InfDbContext _context { get; set; }
    public IPlantRepository PlantRepository { get; set; }
    public IUserRepository UserRepository { get; set; }

    public IDeviceRepository DeviceRepository { get; set; }
    private readonly IPlantSpecificationRepository plantSpecificationRepository;

    public UnitOfWork(InfDbContext context)
    {
        this._context = context;
        this.UserRepository = new UserRepository(context);
        this.PlantRepository = new PlantRepository(context);
        this.DeviceRepository = new DeviceRepository(context);
        this.plantSpecificationRepository = new PlantSpecificationRepostiory(context);
    }

    public async Task SaveChanges() {
        await this._context.SaveChangesAsync();
     }

    public async ValueTask DisposeAsync()
    {
        await this._context.DisposeAsync();
    }
}
