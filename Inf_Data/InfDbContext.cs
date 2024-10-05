using Inf_Data.Entities;
using Inf_Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Inf_Data;

public class InfDbContext : IdentityDbContext<AppUser, AppRoles, int>
{
    public InfDbContext(DbContextOptions<InfDbContext> options)
        : base(options) { }

    protected override async void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    
    }
}
