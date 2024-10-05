using Inf_Data;
using Microsoft.EntityFrameworkCore;

namespace Inf_Repository.Repository.Generic;

public class GenericRepository<T>:IGenericRepository<T> where T : class
{
        private InfDbContext _context { get; set; }
        protected DbSet<T> table;
    public GenericRepository(InfDbContext context)
    {
        this._context=context;
        this.table=context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
    return this.table.AsNoTracking();   
     }

    public async Task<T> GetById(int id)
    {
        return await this.table.FindAsync(id);
    }

    public async Task Create(T entity)
    {
        await table.AddAsync(entity);
    }


    public async Task Delete(int id)
    {
        T existing = await table.FindAsync(id);
        table.Remove(existing);    
    }
}