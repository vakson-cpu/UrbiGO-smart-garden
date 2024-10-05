using Inf_Data.Entities;

namespace Inf_api.Services.Auth
{
    public interface ITokenService
    {
        
        Task<string> GenerateToken(AppUser user,string role);
    }
}