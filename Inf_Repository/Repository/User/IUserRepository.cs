using Inf_Data.Entities;
using Inf_Repository.Repository.Generic;

namespace Inf_Repository.Repository.User;

public interface IUserRepository:IGenericRepository<AppUser>
{
    Task<List<Inf_Data.Entities.AppUser>> GetUsers();
    Task<AppUser> GetUserDetails(int userId);
    Task SaveChanges();
}