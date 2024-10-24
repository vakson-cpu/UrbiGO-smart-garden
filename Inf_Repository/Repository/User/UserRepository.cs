using Inf_Data;
using Inf_Data.Entities;
using Inf_Repository.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace Inf_Repository.Repository.User;

public class UserRepository : GenericRepository<AppUser>,IUserRepository
{
    private  InfDbContext _context { get; set; }
    
    
    public UserRepository(InfDbContext context) : base(context)
    {
        this._context=context;
    }

    public async Task<List<Inf_Data.Entities.AppUser>> GetUsers()
    {
        var result = await _context.Users.ToListAsync();
        return result;
    }
}