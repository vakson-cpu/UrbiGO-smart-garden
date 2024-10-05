
namespace Inf_Data.Entities;
using Microsoft.AspNetCore.Identity;
    public class AppRoles: IdentityRole<int>
    {
        public AppRoles(string name):base(name)
        {
            
        }
    }
