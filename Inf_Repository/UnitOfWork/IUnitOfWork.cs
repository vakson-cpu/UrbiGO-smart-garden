using Inf_Repository.Repository.Generic;
using Inf_Repository.Repository.User;

namespace Inf_Repository.Repository.UnitOfWork;
public interface IUnitOfWork:IAsyncDisposable
{
    IUserRepository UserRepository { get;set; }   
    

    Task SaveChanges();   
}