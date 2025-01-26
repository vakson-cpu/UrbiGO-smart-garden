using Microsoft.AspNetCore.Identity;

namespace Inf_Data.Entities;

public class AppUser: IdentityUser<int> 
{
    public List<Plant> Plants { get; set; } = new();

    public List<Notifications> Notifications { get; set; } = new();
}
