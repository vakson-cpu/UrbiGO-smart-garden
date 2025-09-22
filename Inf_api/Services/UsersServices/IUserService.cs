using Inf_Data.Entities;
using Inf_Transfer.utils;

namespace Inf_api.Services.UserServices
{
    public interface IUserService
    {
        Task<QueryResponse<AppUser>> GetUsers(int pageNumber, int pageSize = 10);
        Task BanUser(int userId);
        Task UnBanUser(int userId);
        Task<AppUser> GetUserDetail(int userId);
    }
}
