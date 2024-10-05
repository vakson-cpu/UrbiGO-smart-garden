using Inf_Data;
using Inf_Repository.Repository.User;

namespace Inf_Repository.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; set; }
    public InfDbContext _context { get; set; }

    public UnitOfWork(InfDbContext context)
    {
        this._context = context;
        this.UserRepository = new UserRepository(context);
    }

    // public async Task Dispose()
    // {
    //     await this._context.DisposeAsync();
    // }

    public async Task SaveChanges() {
        await this._context.SaveChangesAsync();
     }

    public async ValueTask DisposeAsync()
    {
        await this._context.DisposeAsync();
    }
}
